import React, { useState, useEffect } from 'react';
import { getAllProducts } from '../services/api';

const ProductList = () => {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    async function fetchProducts() {
      const response = await getAllProducts();
      setProducts(response.data);
    }
    fetchProducts();
  }, []);

  return (
    <div className="container mt-3">
      <h2>Product List</h2>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Name</th>
            <th>Description</th>
            <th>Images</th>
            <th>Videos</th>
            <th>Ingredients</th>
            <th>Status</th>
            <th>Language</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.ProductId}>
              <td>{product.Name}</td>
              <td>{product.Description}</td>
              <td>
                {product.ImageUrls.map((url, index) => (
                  <img key={index} src={url} alt="Product" width="50" />
                ))}
              </td>
              <td>
                {product.ProductVideoUrls.map((url, index) => (
                  <a key={index} href={url} target="_blank" rel="noopener noreferrer">
                    Video {index + 1}
                  </a>
                ))}
              </td>
              <td>{product.Ingredients}</td>
              <td>{product.Status}</td>
              <td>{product.LanguageName}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ProductList;
