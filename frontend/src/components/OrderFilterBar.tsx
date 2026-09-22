import { useState } from 'react';
import type { OrdersFilter } from '../types';

interface Props {
  onFilter: (filter: OrdersFilter) => void;
}

export default function OrderFilterBar({ onFilter }: Props) {
  const [orderId, setOrderId] = useState('');
  const [customerId, setCustomerId] = useState('');
  const [minAmount, setMinAmount] = useState('');
  const [orderDateFrom, setOrderDateFrom] = useState('');

  function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    const filter: OrdersFilter = {};
    if (orderId) filter.orderId = parseInt(orderId);
    if (customerId) filter.customerId = parseInt(customerId);
    if (minAmount) filter.minAmount = parseFloat(minAmount);
    if (orderDateFrom) filter.orderDateFrom = new Date(orderDateFrom).toISOString();
    onFilter(filter);
  }

  function handleClear() {
    setOrderId('');
    setCustomerId('');
    setMinAmount('');
    setOrderDateFrom('');
    onFilter({});
  }

  return (
    <form className="filter-bar" onSubmit={handleSubmit}>
      <div className="filter-group">
        <label htmlFor="orderId">Order ID</label>
        <input
          id="orderId"
          type="number"
          placeholder="e.g. 3"
          value={orderId}
          onChange={e => setOrderId(e.target.value)}
          min="1"
        />
      </div>
      <div className="filter-group">
        <label htmlFor="customerId">Customer ID</label>
        <input
          id="customerId"
          type="number"
          placeholder="e.g. 2"
          value={customerId}
          onChange={e => setCustomerId(e.target.value)}
          min="1"
        />
      </div>
      <div className="filter-group">
        <label htmlFor="minAmount">Min Amount ($)</label>
        <input
          id="minAmount"
          type="number"
          placeholder="e.g. 100"
          value={minAmount}
          onChange={e => setMinAmount(e.target.value)}
          min="0"
          step="0.01"
        />
      </div>
      <div className="filter-group">
        <label htmlFor="orderDateFrom">Order Date From</label>
        <input
          id="orderDateFrom"
          type="date"
          value={orderDateFrom}
          onChange={e => setOrderDateFrom(e.target.value)}
        />
      </div>
      <div className="filter-actions">
        <button type="submit" className="btn-primary">Filter</button>
        <button type="button" className="btn-secondary" onClick={handleClear}>Clear</button>
      </div>
    </form>
  );
}
