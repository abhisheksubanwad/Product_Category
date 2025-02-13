import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import ProductCategoryForm from './components/ProductCategoryForm';
import ProductSubCategoryForm from './components/ProductSubCategoryForm';
import ProductForm from './components/ProductForm';
import ProductList from './components/ProductList';

function App() {
  return (
    <Router>
      <div className="container mt-4">
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
          <ul className="navbar-nav">
            <li className="nav-item">
              <Link className="nav-link" to="/create-product-category">Create Product Category</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/create-product-subcategory">Create Product SubCategory</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/create-product">Create Product</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/products">Product List</Link>
            </li>
          </ul>
        </nav>

        <Routes>
          <Route path="/create-product-category" element={<ProductCategoryForm />} />
          <Route path="/create-product-subcategory" element={<ProductSubCategoryForm />} />
          <Route path="/create-product" element={<ProductForm />} />
          <Route path="/products" element={<ProductList />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
