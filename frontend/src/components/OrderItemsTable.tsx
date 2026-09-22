import type { OrderItem } from '../types';

interface Props {
  items: OrderItem[];
}

function formatAmount(amount: number) {
  return `$${amount.toFixed(2)}`;
}

export default function OrderItemsTable({ items }: Props) {
  const grandTotal = items.reduce((sum, item) => sum + item.quantity * item.unitPrice, 0);

  return (
    <table className="data-table">
      <thead>
        <tr>
          <th>Product</th>
          <th>Quantity</th>
          <th>Unit Price</th>
          <th>Line Total</th>
        </tr>
      </thead>
      <tbody>
        {items.map(item => (
          <tr key={item.id}>
            <td>{item.product.name}</td>
            <td>{item.quantity}</td>
            <td>{formatAmount(item.unitPrice)}</td>
            <td>{formatAmount(item.quantity * item.unitPrice)}</td>
          </tr>
        ))}
      </tbody>
      <tfoot>
        <tr className="total-row">
          <td colSpan={3}><strong>Grand Total</strong></td>
          <td><strong>{formatAmount(grandTotal)}</strong></td>
        </tr>
      </tfoot>
    </table>
  );
}
