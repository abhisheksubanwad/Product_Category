import React, { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';

const API_BASE_URL = 'https://localhost:7092/api/ProductSubCategory/create'; 
const CATEGORY_API_URL = 'https://localhost:7092/api/ProductCategory/Get-all';

const ProductSubCategoryForm = () => {
  const { register, handleSubmit, reset } = useForm();
  const [categories, setCategories] = useState([]);
  const [message, setMessage] = useState('');
  const [selectedFile, setSelectedFile] = useState(null);

  useEffect(() => {
    async function fetchCategories() {
      try {
        const response = await fetch(CATEGORY_API_URL);
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        const data = await response.json();

        console.log('Fetched Categories:', data);

        if (data.isSuccess) {
          setCategories(data.result || []); // Use 'result' instead of 'Result'
        } else {
          throw new Error(data.message || 'Failed to fetch categories');
        }
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
    // formData.append(`LanguageData[${index}].XYZ`, 'ABC');

    // Handle the image upload and get the URL
    try {
      const imageResponse = await fetch('https://your-upload-image-api-url', {
        method: 'POST',
        body: selectedFile, // Adjust to match how your backend expects the file to be uploaded
      });

      if (!imageResponse.ok) {
        throw new Error('Failed to upload image');
      }

      const imageData = await imageResponse.json(); // Assuming the response contains the image URL
      const imageUrl = imageData.url; // Replace with actual response structure

      formData.append('ImageUrl', imageUrl); // Add image URL to the form data

      // Include the LanguageData (modify according to your backend's expected format)
      const languageData = {
        language: data.LanguageName,
        title: data.Title,
        description: data.Description,
      };

      formData.append('LanguageData', JSON.stringify(languageData)); // Include LanguageData field

      // Submit the form data to the backend
      const response = await fetch(API_BASE_URL, {
        method: 'POST',
        body: formData,
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Failed to create Product SubCategory');
      }

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
            {categories.length > 0 ? (
              categories.map((cat) => (
                <option key={cat.productCategoryId} value={cat.productCategoryId}>
                  {cat.title}
                </option>
              ))
            ) : (
              <option>No categories available</option>
            )}
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
