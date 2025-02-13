import React, { useState } from 'react';
import { createProductCategory } from '../services/api';
import { useForm } from 'react-hook-form';

const ProductCategoryForm = () => {
  const { register, handleSubmit, reset } = useForm();
  const [message, setMessage] = useState('');
  const [imageFile, setImageFile] = useState(null);

  // Handle image selection
  const handleImageChange = (event) => {
    setImageFile(event.target.files[0]);
  };

  const onSubmit = async (data) => {
    try {
      const formData = new FormData();
      formData.append('Title', data.Title);
      formData.append('Description', data.Description);
      formData.append('LanguageName', data.LanguageName);
      if (imageFile) {
        formData.append('Image', imageFile); // Append the image file
      }

      await createProductCategory(formData);

      setMessage('Product Category Created Successfully!');
      reset();
      setImageFile(null); // Reset image selection
    } catch (error) {
      setMessage('Error: ' + error.message);
    }
  };

  return (
    <div className="container mt-3">
      <h2>Create Product Category</h2>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="mb-3">
          <label>Title:</label>
          <input type="text" className="form-control" {...register('Title')} required />
        </div>
        <div className="mb-3">
          <label>Description:</label>
          <textarea className="form-control" {...register('Description')} required />
        </div>
        <div className="mb-3">
          <label>Image:</label>
          <input type="file" className="form-control" accept="image/*" onChange={handleImageChange} />
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

export default ProductCategoryForm;
