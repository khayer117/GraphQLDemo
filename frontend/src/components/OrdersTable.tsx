import { useNavigate } from 'react-router-dom';
import type { Order } from '../types';

interface Props {
  orders: Order[];
}

function formatDate(iso: string) {
  return new Date(iso).toLocaleDateString();
}

function formatAmount(amount: number) {
  return `$${amount.toFixed(2)}`;
}

export default function OrdersTable({ orders }: Props) {
  const navigate = useNavigate();

  if (orders.length === 0) {
    return <p className="empty-message">No orders match the current filters.</p>;
  }

  return (
    <table className="data-table">
      <thead>
        <tr>
          <th>Order ID</th>
          <th>Customer</th>
          <th>Order Date</th>
          <th>Total Amount</th>
        </tr>
      </thead>
      <tbody>
        {orders.map(order => (
          <tr key={order.id} onClick={() => navigate(`/orders/${order.id}`)} className="clickable-row">
            <td>
              <span className="order-id-link">#{order.id}</span>
            </td>
            <td>{order.customer.name}</td>
            <td>{formatDate(order.orderDate)}</td>
            <td>{formatAmount(order.totalAmount)}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
