import React, { useState, useEffect } from 'react';
import { createProduct, getAllProductSubCategories } from '../services/api';
import { useForm } from 'react-hook-form';

const ProductForm = () => {
  const { register, handleSubmit, reset } = useForm();
  const [subCategories, setSubCategories] = useState([]);
  const [message, setMessage] = useState('');
  const [selectedImages, setSelectedImages] = useState([]);
  const [videoUrls, setVideoUrls] = useState(['']); // Start with one input field

  useEffect(() => {
    async function fetchSubCategories() {
      const response = await getAllProductSubCategories();
      setSubCategories(response.data);
    }
    fetchSubCategories();
  }, []);

  // Handle file selection for images
  const handleImageChange = (e) => {
    setSelectedImages(e.target.files); // Store the selected files
  };

  // Handle adding new video URL fields
  const addVideoUrlField = () => {
    setVideoUrls([...videoUrls, '']);
  };

  // Handle changes in video URL fields
  const handleVideoUrlChange = (index, value) => {
    const updatedUrls = [...videoUrls];
    updatedUrls[index] = value;
    setVideoUrls(updatedUrls);
  };

  // Handle form submission
  const onSubmit = async (data) => {
    try {
      const formData = new FormData();
      formData.append('ProductSubCategoryId', data.ProductSubCategoryId);
      formData.append('Name', data.Name);
      formData.append('Description', data.Description);
      formData.append('Ingredients', data.Ingredients);
      formData.append('Status', data.Status);
      formData.append('LanguageName', data.LanguageName);

      // Append multiple images
      if (selectedImages.length > 0) {
        for (let i = 0; i < selectedImages.length; i++) {
          formData.append('Images', selectedImages[i]);
        }
      }

      // Append multiple video URLs
      formData.append('ProductVideoUrls', JSON.stringify(videoUrls.filter(url => url.trim() !== '')));

      await createProduct(formData);
      setMessage('Product Created Successfully!');
      reset();
      setSelectedImages([]);
      setVideoUrls(['']);
    } catch (error) {
      setMessage('Error: ' + error.message);
    }
  };

  return (
    <div className="container mt-3">
      <h2>Create Product</h2>
      <form onSubmit={handleSubmit(onSubmit)} encType="multipart/form-data">
        <div className="mb-3">
          <label>SubCategory:</label>
          <select className="form-control" {...register('ProductSubCategoryId')} required>
            <option value="">Select SubCategory</option>
            {subCategories.map((subCat) => (
              <option key={subCat.ProductSubCategoryId} value={subCat.ProductSubCategoryId}>
                {subCat.Title}
              </option>
            ))}
          </select>
        </div>

        <div className="mb-3">
          <label>Product Name:</label>
          <input type="text" className="form-control" {...register('Name')} required />
        </div>

        <div className="mb-3">
          <label>Description:</label>
          <textarea className="form-control" {...register('Description')} required />
        </div>

        {/* Image Upload Field */}
        <div className="mb-3">
          <label>Upload Images (Multiple):</label>
          <input type="file" className="form-control" multiple onChange={handleImageChange} />
        </div>

        {/* Video URLs Input Fields */}
        <div className="mb-3">
          <label>Product Video URLs:</label>
          {videoUrls.map((url, index) => (
            <div key={index} className="d-flex mb-2">
              <input
                type="text"
                className="form-control"
                value={url}
                onChange={(e) => handleVideoUrlChange(index, e.target.value)}
                placeholder="Enter video URL"
              />
              {index === videoUrls.length - 1 && (
                <button type="button" className="btn btn-secondary ms-2" onClick={addVideoUrlField}>
                  +
                </button>
              )}
            </div>
          ))}
        </div>

        <div className="mb-3">
          <label>Ingredients:</label>
          <textarea className="form-control" {...register('Ingredients')} />
        </div>

        <div className="mb-3">
          <label>Status:</label>
          <select className="form-control" {...register('Status')} required>
            <option value="Active">Active</option>
            <option value="Inactive">Inactive</option>
          </select>
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

export default ProductForm;
