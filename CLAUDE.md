# Order Management Demo — GraphQL + React

## Overview

A small demo application to browse orders and their line items.

- **Parent page**: a list of all orders, with filters (order ID, minimum amount, order date, customer ID).
- **Child page**: clicking an order opens its detail page, showing the order's line items (products, quantities, prices).

Keep the implementation simple and readable — this is a learning/demo project, not a production system. Favor clarity over abstraction (no repository-pattern-over-EF-Core, no CQRS, no auth).

## Tech Stack

**Backend**
- .NET 10 / ASP.NET Core (minimal hosting model, `Program.cs`)
- Entity Framework Core, code-first migrations
- SQLite (single file, e.g. `orders.db`, created automatically via migrations on startup)
- HotChocolate for GraphQL (single `/graphql` endpoint)

**Frontend**
- React 18 + TypeScript, scaffolded with Vite
- React Router (two routes: orders list, order detail)
- Apollo Client for GraphQL queries
- Plain CSS (no component library needed) — clean and minimal is fine

## Project Structure

```
/backend
  OrderDemo.Api/
    OrderDemo.Api.csproj
    Program.cs
    Data/
      AppDbContext.cs
      SeedData.cs
    Models/
      Customer.cs
      Product.cs
      Order.cs
      OrderItem.cs
    GraphQL/
      Query.cs
      Types/
        OrderType.cs
        OrderItemType.cs
      Filters/
        OrdersFilterInput.cs
    Migrations/
/frontend
  index.html
  package.json
  src/
    main.tsx
    App.tsx
    apolloClient.ts
    graphql/
      queries.ts
    pages/
      OrdersListPage.tsx
      OrderDetailPage.tsx
    components/
      OrderFilterBar.tsx
      OrdersTable.tsx
      OrderItemsTable.tsx
    types.ts
```

## Data Model

**Customer**
- `Id` (int, PK)
- `Name` (string)
- `Email` (string)

**Product**
- `Id` (int, PK)
- `Name` (string)
- `Price` (decimal) — current catalog price

**Order**
- `Id` (int, PK)
- `OrderDate` (DateTime)
- `CustomerId` (FK → Customer)
- Navigation: `Customer`, `OrderItems`
- No stored `Amount` column — total is **computed** as the sum of `OrderItems.Quantity * OrderItems.UnitPrice`, exposed via a GraphQL resolver field (`totalAmount`) on the Order type. This is intentional: it's a good example of a GraphQL field that doesn't map 1:1 to a database column.

**OrderItem**
- `Id` (int, PK)
- `OrderId` (FK → Order)
- `ProductId` (FK → Product)
- `Quantity` (int)
- `UnitPrice` (decimal) — price *at the time of the order* (copied from Product.Price when the order was created; deliberately decoupled from the live catalog price, since that's how real order systems work)
- Navigation: `Order`, `Product`

## Backend Requirements

1. **DbContext & migrations**: `AppDbContext` with `DbSet<Customer>`, `DbSet<Product>`, `DbSet<Order>`, `DbSet<OrderItem>`. Use SQLite (`UseSqlite`). Apply migrations automatically on startup (`db.Database.Migrate()`) so the file just runs with no manual setup step.

2. **Seed data**: on startup, if the database is empty, seed:
   - ~5 customers
   - ~8 products with varied prices
   - ~12 orders spread across different dates (some past week, some past month) and different customers
   - 1–4 order items per order, referencing the seeded products

   This gives the frontend filters something realistic to exercise.

3. **GraphQL schema** (HotChocolate):
   - `Query.orders(filter: OrdersFilterInput): [Order!]!` — returns orders matching the filter, each including `customer` and `totalAmount` (items are *not* required for the list view, keep that payload light).
   - `Query.orderById(id: Int!): Order` — returns a single order including its `orderItems` (each with `product` details), used by the detail page.

   **OrdersFilterInput** fields (all optional — an unset field means "don't filter on this"):
   - `orderId: Int`
   - `customerId: Int`
   - `minAmount: Decimal` — orders whose computed total is **greater than or equal to** this value
   - `orderDateFrom: DateTime` — orders placed **on or after** this date

   Note on `orderDateFrom`: the requirement just says "filter by order date" without specifying exact vs. range — treat it as "on or after" since that's the more generally useful behavior for a list view. Feel free to rename to `orderDate` and use exact-date matching instead if that's what's actually wanted; flag this assumption back to the user rather than silently guessing further.

4. **Filtering implementation**: since `totalAmount` is computed rather than a column, applying `minAmount` purely in SQL requires either a raw SQL projection or grouping. For this demo's scale (a dozen or so orders), it's acceptable to: apply `orderId`/`customerId`/`orderDateFrom` as an EF Core `IQueryable` filter (translates to SQL), then materialize with `Include(OrderItems)`, then apply the `minAmount` check in-memory over the computed total. Add a short code comment noting this is a simplification that wouldn't scale to a large orders table (you'd want a computed/denormalized total column or SQL-side aggregation instead).

5. **CORS**: enable CORS for the Vite dev server's origin (`http://localhost:5173` by default) so the frontend can call `/graphql` directly during development.

6. **Ports**: run the API on a fixed port (e.g. `http://localhost:5080`) so the frontend's Apollo Client config doesn't have to guess.

## Frontend Requirements

1. **Orders List page** (`/`):
   - A filter bar with four inputs: Order ID (number), Min Amount (number), Order Date From (date picker), Customer ID (number). All optional; a "Clear filters" action. Filters should re-run the GraphQL query (client-side is fine — no need to debounce heavily for a demo, but don't fire a query on every keystroke either; a "Filter" button or simple debounce is fine).
   - A table of matching orders: Order ID, Customer Name, Order Date, Total Amount.
   - Clicking an order's ID (or its row) navigates to `/orders/:id`.

2. **Order Detail page** (`/orders/:id`):
   - Header showing Order ID, Order Date, Customer Name.
   - A table of that order's line items: Product Name, Quantity, Unit Price, Line Total (`Quantity * UnitPrice`), plus the order's grand total at the bottom.
   - A "Back to orders" link/button that returns to `/`.

3. **Apollo Client**: single client instance pointed at `http://localhost:5080/graphql`, provided at the app root.

4. **Styling**: minimal, clean plain CSS — a readable table, a simple filter bar layout. No need for Tailwind/MUI/etc. unless that's easier for you to produce well; simplicity matters more than polish here.

## Dev Workflow

```bash
# Backend
cd backend/OrderDemo.Api
dotnet run
# → API + GraphQL at http://localhost:5080/graphql (Banana Cake Pop IDE available there too)

# Frontend (separate terminal)
cd frontend
npm install
npm run dev
# → UI at http://localhost:5173
```

## Acceptance Checklist

- [ ] `dotnet run` creates/migrates `orders.db` and seeds sample data with no manual steps
- [ ] `/graphql` responds to an `orders` query and an `orderById` query
- [ ] Orders list page loads and displays seeded orders
- [ ] All four filters (order ID, min amount, order date, customer ID) work, individually and combined
- [ ] Clicking an order navigates to its detail page and shows the correct line items
- [ ] Detail page's line-item totals and grand total are arithmetically correct
- [ ] Backend and frontend run concurrently without CORS errors

## Out of Scope (don't build these)

- Authentication / authorization
- Pagination or infinite scroll (the seeded dataset is small)
- Editing/creating orders from the UI (read-only demo)
- Production deployment concerns (Docker, CI/CD, env-based config)
