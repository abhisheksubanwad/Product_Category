import React, { useState } from 'react';
import { useForm } from 'react-hook-form';

const API_BASE_URL = 'https://localhost:7092/api/ProductCategory/create';

const ProductCategoryForm = () => {
  const { handleSubmit, reset } = useForm();
  const [message, setMessage] = useState('');
  const [imageFile, setImageFile] = useState(null);
  const [languageEntries, setLanguageEntries] = useState([
    { language: 0, Title: '', Description: '' }
  ]);

  const handleImageChange = (event) => {
    setImageFile(event.target.files[0]);
  };

  const addLanguageEntry = () => {
    setLanguageEntries([...languageEntries, { language: 0, Title: '', Description: '' }]);
  };

  const handleLanguageChange = (index, field, value) => {
    const updatedEntries = [...languageEntries];
    updatedEntries[index][field] = value;
    setLanguageEntries(updatedEntries);
  };

  const createProductCategory = async (formData) => {
    try {
      const response = await fetch(API_BASE_URL, {
        method: 'POST',
        body: formData,
      });

      if (!response.ok) {
        throw new Error('Failed to create product category');
      }

      return await response.json();
    } catch (error) {
      throw new Error(error.message);
    }
  };

  const onSubmit = async () => {
    try {
      const formData = new FormData();

      // Map individual language entry fields
      languageEntries.forEach((entry, index) => {
        formData.append(`LanguageData[${index}].language`, entry.language);
        formData.append(`LanguageData[${index}].Title`, entry.Title);
        formData.append(`LanguageData[${index}].Description`, entry.Description);
        formData.append(`LanguageData[${index}].XYZ`, 'ABC');
      });

      // Append image with correct key ("ImageUrl" or "Image" depending on API)
      if (imageFile) {
        formData.append('ImageUrl', imageFile);
      } else {
        const blob = new Blob([''], { type: 'image/png' });
        formData.append('ImageUrl', blob, 'placeholder.png');
      }

      for (let pair of formData.entries()) {
        console.log(pair[0], pair[1]); // Debugging
      }

      await createProductCategory(formData);

      setMessage('Product Category Created Successfully!');
      reset();
      setImageFile(null);
      setLanguageEntries([{ language: 0, Title: '', Description: '' }]);
    } catch (error) {
      setMessage('Error: ' + error.message);
    }
  };

  return (
    <div className="container mt-3">
      <h2>Create Product Category</h2>
      <form onSubmit={handleSubmit(onSubmit)}>
        {languageEntries.map((entry, index) => (
          <div key={index} className="mb-3 border p-3">
            <label>Language:</label>
            <select
              className="form-control mb-2"
              value={entry.language}
              onChange={(e) => handleLanguageChange(index, 'language', Number(e.target.value))}
              required
            >
              <option value="0">English</option>
              <option value="1">Marathi</option>
              <option value="2">Hindi</option>
            </select>
            <label>Title:</label>
            <input
              type="text"
              className="form-control mb-2"
              value={entry.Title}
              onChange={(e) => handleLanguageChange(index, 'Title', e.target.value)}
              required
            />
            <label>Description:</label>
            <textarea
              className="form-control mb-2"
              value={entry.Description}
              onChange={(e) => handleLanguageChange(index, 'Description', e.target.value)}
              required
            />
          </div>
        ))}
        <button type="button" className="btn btn-secondary mb-3" onClick={addLanguageEntry}>
          Add Another Language
        </button>
        <div className="mb-3">
          <label>Image:</label>
          <input type="file" className="form-control" accept="image/*" onChange={handleImageChange} />
        </div>
        <button type="submit" className="btn btn-primary">Create</button>
      </form>
      {message && <p className="mt-2">{message}</p>}
    </div>
  );
};

export default ProductCategoryForm;