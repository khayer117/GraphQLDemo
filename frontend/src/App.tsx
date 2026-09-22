import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { ApolloProvider } from '@apollo/client/react';
import client from './apolloClient';
import OrdersListPage from './pages/OrdersListPage';
import OrderDetailPage from './pages/OrderDetailPage';
import './App.css';

export default function App() {
  return (
    <ApolloProvider client={client}>
      <BrowserRouter>
        <div className="app">
          <header className="app-header">
            <span className="app-title">Order Management Demo</span>
          </header>
          <main className="app-main">
            <Routes>
              <Route path="/" element={<OrdersListPage />} />
              <Route path="/orders/:id" element={<OrderDetailPage />} />
            </Routes>
          </main>
        </div>
      </BrowserRouter>
    </ApolloProvider>
  );
}
