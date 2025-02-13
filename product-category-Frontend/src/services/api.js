import axios from 'axios';

const API_BASE_URL = 'https://localhost:7092/api'; // Change this URL as per your backend

export const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Product Category API Calls
export const createProductCategory = async (data) => api.post('/ProductCategory/create', data);
export const getAllProductCategories = async () => api.get('//ProductCategory/Get-all');

// Product SubCategory API Calls
export const createProductSubCategory = async (data) => api.post('/ProductSubCategory/create', data);
export const getAllProductSubCategories = async () => api.get('/ProductSubCategory/get-all');

// Product API Calls
export const createProduct = async (data) => api.post('/Product/create', data);
export const getAllProducts = async () => api.get('/Product/get-all');
