import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { ApiService } from './ApiService';
import './App.css';

interface Employee {
    id: number;
    name: string;
    cpf: string;
    email: string;
    phone: string;
}

const Employee: React.FC = () => {
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const [company, setCompany] = useState<{ name?: string } | null>(null);
    const [isCompanyLoading, setIsCompanyLoading] = useState(true);
    const employeeEmptyRow: Employee = { id: 0, name: '', cpf: '', email: '', phone: '' };
    let employeesWithEmptyRow: Employee[] = [];

    useEffect(() => {
        fetchCompany();
        fetchEmployees();
    }, []);

    const fetchCompany = async () => {
        try {
            const data = await ApiService.get('company');
            setCompany(data);
        } catch (error) {
            setCompany(null);
        } finally {
            setIsCompanyLoading(false);
        }
    };

    const fetchEmployees = async () => {
        setIsLoading(true);
        setError('');
        try {
            const data = await ApiService.get<Employee[]>('employee');
            setEmployees(data);
        } catch (err: any) {
            setError(err.message || 'Erro ao buscar funcionários');
        } finally {
            setIsLoading(false);
        }
    };

    const saveEmployee = async (employee: Employee) => {
        setIsLoading(true);
        const isNew = employee.id == 0 || employee.id == undefined;
        try {
            const endpoint = isNew ? 'employee' : `employee/${employee.id}`;
            await ApiService[isNew ? 'post' : 'put'](endpoint, employee);
            fetchEmployees();
        } catch (err: any) {
            setError(err.message || 'Erro ao salvar funcionário');
        } finally {
            setIsLoading(false);
        }
    };

    const deleteEmployee = async (id: number) => {
        setIsLoading(true);
        try {
            await ApiService.delete(`employee/${id}`);
            fetchEmployees();
        } catch (err: any) {
            setError(err.message || 'Erro ao deletar funcionário');
        } finally {
            setIsLoading(false);
        }
    };

    const updateField = (index: number, field: keyof Employee, value: string) => {
        const updatedEmployees = [...employees];
        updatedEmployees[index] = { ...updatedEmployees[index], [field]: value };
        setEmployees(updatedEmployees);
    };

    const handleCpfChange = (e: React.ChangeEvent<HTMLInputElement>, index: number) => {
        const value = e.target.value.replace(/\D/g, '');
        updateField(index, 'cpf', value);
    };

    const addEmptyRow = () => {
        employeesWithEmptyRow.push(employeeEmptyRow);
    }

    employeesWithEmptyRow = [...employees];
    if (!employees.some(obj => obj.id === employeeEmptyRow.id && obj.name === employeeEmptyRow.name && obj.cpf === employeeEmptyRow.cpf && obj.email === employeeEmptyRow.email && obj.phone === employeeEmptyRow.phone) &&
        employees.every((obj) => 'id' in obj && obj.id > 0))
        addEmptyRow();

    const handleLogout = () => {
        localStorage.removeItem('authToken'); 
        window.location.href = '/'; 
    };

    return (
        <div className="employee-container">
            <button
                className="logout-button"
                onClick={handleLogout}
            >
                Deslogar
            </button>
            {isLoading && (
                <div className="loading-overlay">
                    <div className="spinner"></div>
                </div>
            )}
            <h1>
                {isCompanyLoading ? (
                    'Carregando empresa...'
                ) : company && company.name ? (
                    <>
                        <span>{company.name}</span>{' '}
                        <Link to="/company" className="company-details-btn"  state={{ company }}>
                            Detalhes
                        </Link>
                    </>
                ) : (
                    <>
                        <Link to="/company" className="register-company-btn">
                            Cadastrar Empresa
                        </Link>
                    </>
                )}
            </h1>

            {error && <p className="error">{error}</p>}
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Nome</th>
                        <th>CPF</th>
                        <th>Email</th>
                        <th>Telefone</th>
                        <th>Ações</th>
                    </tr>
                </thead>
                <tbody>
                    {employeesWithEmptyRow.map((employee, index) => (
                        <tr key={index}>
                            <td>{employee.id || '-'}</td>
                            <td>
                                <input
                                    type="text"
                                    value={employee.name}
                                    onChange={(e) => updateField(index, 'name', e.target.value)}
                                />
                            </td>
                            <td>
                                <input
                                    type="text"
                                    value={employee.cpf}
                                    onChange={(e) => handleCpfChange(e, index)}
                                />
                            </td>
                            <td>
                                <input
                                    type="email"
                                    value={employee.email}
                                    onChange={(e) => updateField(index, 'email', e.target.value)}
                                />
                            </td>
                            <td>
                                <input
                                    type="text"
                                    value={employee.phone}
                                    onChange={(e) => updateField(index, 'phone', e.target.value)}
                                />
                            </td>
                            <td className="actions-btns">
                                {employee.id == 0 || employee.id == undefined ? (
                                    <button className="add-btn" onClick={() => saveEmployee(employee)}>
                                        Adicionar
                                    </button>
                                ) : (
                                    <>
                                        <button className="edit-btn" onClick={() => saveEmployee(employee)}>
                                            Editar
                                        </button>
                                        <button className="delete-btn" onClick={() => deleteEmployee(employee.id)}>
                                            Apagar
                                        </button>
                                    </>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Employee;
