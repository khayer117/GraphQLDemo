export interface Customer {
  id: number;
  name: string;
  email: string;
}

export interface Product {
  id: number;
  name: string;
  price: number;
}

export interface OrderItem {
  id: number;
  productId: number;
  product: Product;
  quantity: number;
  unitPrice: number;
}

export interface Order {
  id: number;
  orderDate: string;
  customerId: number;
  customer: Customer;
  orderItems: OrderItem[];
  totalAmount: number;
}

export interface OrdersFilter {
  orderId?: number;
  customerId?: number;
  minAmount?: number;
  orderDateFrom?: string;
}
