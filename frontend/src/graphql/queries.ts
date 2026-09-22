import { gql } from '@apollo/client';

export const GET_ORDERS = gql`
  query GetOrders($filter: OrdersFilterInput) {
    orders(filter: $filter) {
      id
      orderDate
      customerId
      customer {
        id
        name
      }
      totalAmount
    }
  }
`;

export const GET_ORDER_BY_ID = gql`
  query GetOrderById($id: Int!) {
    orderById(id: $id) {
      id
      orderDate
      customer {
        id
        name
        email
      }
      orderItems {
        id
        quantity
        unitPrice
        product {
          id
          name
        }
      }
      totalAmount
    }
  }
`;
