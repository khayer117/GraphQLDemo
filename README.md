# Order Management Demo

A learning project demonstrating GraphQL API design with HotChocolate on ASP.NET Core, consumed by a React + TypeScript frontend via Apollo Client.

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | .NET 10, ASP.NET Core, HotChocolate, Entity Framework Core, SQLite |
| Frontend | React 18, TypeScript, Vite, Apollo Client, React Router |

## Project Structure

```
GraphQLDemo/
├── backend/
│   └── OrderDemo.Api/
│       ├── Program.cs
│       ├── Data/          # DbContext + seed data
│       ├── Models/        # Customer, Product, Order, OrderItem
│       ├── GraphQL/       # Query, types, filters
│       └── Migrations/
└── frontend/
    └── src/
        ├── pages/         # OrdersListPage, OrderDetailPage
        ├── components/    # OrderFilterBar, OrdersTable, OrderItemsTable
        ├── graphql/       # Apollo queries
        └── types.ts
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)

### Backend

```bash
cd backend/OrderDemo.Api
dotnet run
```

- API runs at `http://localhost:5080`
- GraphQL endpoint: `http://localhost:5080/graphql`
- GraphQL IDE (Banana Cake Pop): `http://localhost:5080/graphql`
- SQLite database (`orders.db`) is created and seeded automatically on first run

### Frontend

```bash
cd frontend
npm install
npm run dev
```

- UI runs at `http://localhost:5173`

Run both concurrently in separate terminals.

## Features

- **Orders list** — browse all orders with filters: Order ID, Customer ID, Min Amount, Order Date From
- **Order detail** — click any order to see its line items, unit prices, and grand total
- **Computed totals** — order total is calculated from line items (not stored), demonstrating a GraphQL resolver field

## GraphQL API

### Queries

```graphql
# List orders with optional filters
query {
  orders(filter: {
    orderId: 1
    customerId: 2
    minAmount: 50.00
    orderDateFrom: "2024-01-01"
  }) {
    id
    orderDate
    totalAmount
    customer { name }
  }
}

# Single order with line items
query {
  orderById(id: 1) {
    id
    orderDate
    customer { name email }
    orderItems {
      quantity
      unitPrice
      product { name }
    }
  }
}
```

## Data Model

- **Customer** — id, name, email
- **Product** — id, name, price (current catalog price)
- **Order** — id, orderDate, customerId; `totalAmount` is a computed GraphQL field (sum of line items)
- **OrderItem** — id, orderId, productId, quantity, unitPrice (price at time of order, independent of current catalog price)
