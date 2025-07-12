// src/api.js
import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:44360', // ✅ CORRETO
})

// Interceptador opcional para adicionar JWT
api.interceptors.request.use(config => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

export default api

// Funções da API usando a instância
export const getAllStudents = () => api.get('/getAll');

export const deleteStudentByRA = (ra) => api.post(`/delete/${ra}`);

export const addStudent = (student) => api.post('/add', student);

export const getStudentByRA = (ra) => api.get(`/GetByRA/${ra}`);

export const editStudent = (student) => api.post('/edit', student);

export const getByName = (name) => {
  return api.get(`/getByName?name=${encodeURIComponent(name)}`)
}
