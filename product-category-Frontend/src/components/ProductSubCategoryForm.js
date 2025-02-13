import React, { useState, useEffect } from 'react';
import { createProductSubCategory, getAllProductCategories } from '../services/api';
import { useForm } from 'react-hook-form';

const ProductSubCategoryForm = () => {
  const { register, handleSubmit, reset } = useForm();
  const [categories, setCategories] = useState([]);
  const [message, setMessage] = useState('');
  const [selectedFile, setSelectedFile] = useState(null);

  useEffect(() => {
    async function fetchCategories() {
      try {
        const response = await getAllProductCategories();
        setCategories(response.data);
      } catch (error) {
        console.error('Error fetching categories:', error);
      }
    }
    fetchCategories();
  }, []);

  const onSubmit = async (data) => {
    if (!selectedFile) {
      setMessage('Please select an image file.');
      return;
    }

    const formData = new FormData();
    formData.append('ProductCategoryId', data.ProductCategoryId);
    formData.append('Title', data.Title);
    formData.append('Description', data.Description);
    formData.append('LanguageName', data.LanguageName);
    formData.append('ImageFile', selectedFile); // Append selected file

    try {
      await createProductSubCategory(formData);
      setMessage('Product SubCategory Created Successfully!');
      reset();
      setSelectedFile(null);
    } catch (error) {
      setMessage('Error: ' + error.message);
    }
  };

  return (
    <div className="container mt-3">
      <h2>Create Product SubCategory</h2>
      <form onSubmit={handleSubmit(onSubmit)} encType="multipart/form-data">
        <div className="mb-3">
          <label>Category:</label>
          <select className="form-control" {...register('ProductCategoryId')} required>
            <option value="">Select Category</option>
            {categories.map((cat) => (
              <option key={cat.ProductCategoryId} value={cat.ProductCategoryId}>
                {cat.Title}
              </option>
            ))}
          </select>
        </div>
        <div className="mb-3">
          <label>Title:</label>
          <input type="text" className="form-control" {...register('Title')} required />
        </div>
        <div className="mb-3">
          <label>Description:</label>
          <textarea className="form-control" {...register('Description')} required />
        </div>
        <div className="mb-3">
          <label>Upload Image:</label>
          <input
            type="file"
            className="form-control"
            accept="image/*"
            onChange={(e) => setSelectedFile(e.target.files[0])}
            required
          />
        </div>
        <div className="mb-3">
          <label>Language:</label>
          <select className="form-control" {...register('LanguageName')} required>
            <option value="EN">English</option>
            <option value="MR">Marathi</option>
            <option value="HI">Hindi</option>
          </select>
        </div>
        <button type="submit" className="btn btn-primary">Create</button>
      </form>
      {message && <p className="mt-2">{message}</p>}
    </div>
  );
};

export default ProductSubCategoryForm;
