import { useParams, Link } from 'react-router-dom';
import { useQuery } from '@apollo/client/react';
import { GET_ORDER_BY_ID } from '../graphql/queries';
import OrderItemsTable from '../components/OrderItemsTable';
import type { Order } from '../types';

function formatDate(iso: string) {
  return new Date(iso).toLocaleDateString();
}

export default function OrderDetailPage() {
  const { id } = useParams<{ id: string }>();
  const orderId = parseInt(id ?? '0');

  const { loading, error, data } = useQuery<{ orderById: Order | null }>(GET_ORDER_BY_ID, {
    variables: { id: orderId },
  });

  if (loading) return <p className="status-message">Loading order...</p>;
  if (error) return <p className="error-message">Error: {error.message}</p>;

  const order = data?.orderById;
  if (!order) return <p className="error-message">Order #{orderId} not found.</p>;

  return (
    <div className="page">
      <Link to="/" className="back-link">← Back to orders</Link>
      <div className="order-header">
        <h1>Order #{order.id}</h1>
        <div className="order-meta">
          <span><strong>Date:</strong> {formatDate(order.orderDate)}</span>
          <span><strong>Customer:</strong> {order.customer.name}</span>
          <span><strong>Email:</strong> {order.customer.email}</span>
        </div>
      </div>
      <h2>Line Items</h2>
      <OrderItemsTable items={order.orderItems} />
    </div>
  );
}
