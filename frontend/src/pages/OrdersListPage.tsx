import { useState } from 'react';
import { useQuery } from '@apollo/client/react';
import { GET_ORDERS } from '../graphql/queries';
import OrderFilterBar from '../components/OrderFilterBar';
import OrdersTable from '../components/OrdersTable';
import type { Order, OrdersFilter } from '../types';

export default function OrdersListPage() {
  const [filter, setFilter] = useState<OrdersFilter>({});

  const { loading, error, data } = useQuery<{ orders: Order[] }>(GET_ORDERS, {
    variables: { filter: Object.keys(filter).length > 0 ? filter : null },
  });

  return (
    <div className="page">
      <h1>Orders</h1>
      <OrderFilterBar onFilter={setFilter} />
      {loading && <p className="status-message">Loading orders...</p>}
      {error && <p className="error-message">Error: {error.message}</p>}
      {data && <OrdersTable orders={data.orders} />}
    </div>
  );
}
