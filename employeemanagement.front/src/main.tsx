import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import App from './App';
import Register from './Register';
import Employees from './Employee';
import Company from './Company';
import PrivateRoute from './PrivateRoute'; 

function Main() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<App />} />
                <Route path="/register" element={<Register />} />
                <Route
                    path="/company"
                    element={<PrivateRoute element={<Company />} />}
                />
                <Route
                    path="/employees"
                    element={<PrivateRoute element={<Employees />} />}
                />
            </Routes>
        </Router>
    );
}

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <Main />
    </React.StrictMode>
);
