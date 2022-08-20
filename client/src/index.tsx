import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import InputPage from './Components/InputPage';
import ErrorPage from './Components/ErrorPage';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <BrowserRouter>
     <Routes>
      <Route path="/" element={<App />}>
        <Route path="" element={<InputPage/>}/>
        <Route path="*" element={<ErrorPage/>}/>
      </Route>
     </Routes>
    </BrowserRouter>
  </React.StrictMode>
);
