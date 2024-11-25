import React, { useState, useEffect } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { ApiService } from './ApiService';

const CompanyDetails: React.FC = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const companyId: number = location.state ? location.state.company.id : 0;

    const [company, setCompany] = useState({
        id: 0,
        name: '',
        cnpj: '',
        address: '',
        email: '',
        site: '',
        phone: '',
    });

    const [editField, setEditField] = useState<string | null>(null);
    const [error, setError] = useState('');

    useEffect(() => {
        if (companyId > 0)
            setCompany(location.state.company);

    }, [companyId]);


    const handleEditClick = (field: string) => {
        setEditField(field);
    };

    const handleBlur = () => {
        setEditField(null); 
    };

    const handleInputChange = (field: keyof typeof company, value: string) => {
        setCompany({ ...company, [field]: value });
    };

    const saveCompany = async () => {
        setError('');
        try {
            if (companyId) {
                await ApiService.put(`company/${companyId}`, company);
            } else {
                await ApiService.post('company', company);
            }
            navigate('/employees');
        } catch (err: any) {
            setError('Erro ao salvar os dados da empresa.');
        }
    };

    const handleLogout = () => {
        localStorage.removeItem('authToken');
        window.location.href = '/';
    };

    const renderField = (field: keyof typeof company, label: string) => {
        if (editField === field || companyId == 0) {
            return (
                <div>
                    <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', marginBottom: '10px' }}>
                        <label>{label}:</label>
                        <input
                            type="text"
                            className="input-data-company"
                            value={company[field]}
                            onChange={(e) => handleInputChange(field, e.target.value)}
                            onBlur={handleBlur}
                            autoFocus
                        />
                    </div>
                </div>
            );
        } else {
            return (
                <div>
                    <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
                        <label>{label}:</label>

                        <div className="lbl-data-company">
                            <span>{company[field] || '-'}</span>
                        </div>
                        <button
                            className="btn-pencil-edit"
                            onClick={() => handleEditClick(field)}
                            aria-label={`Editar ${label}`}
                        >
                            🖉
                        </button>
                    </div>
                </div>
            );
        }
    };

    return (
        <div>
            <button
                className="back-button"
                onClick={() => window.history.back()}
            >
                Voltar
            </button>
            <button
                className="logout-button"
                onClick={handleLogout}
            >
                Deslogar
            </button>
            <h1>{companyId ? 'Editar Empresa' : 'Cadastrar Nova Empresa'}</h1>
            {error && <p className="error">{error}</p>}
            <form
                onSubmit={(e) => {
                    e.preventDefault();
                    saveCompany();
                }}
            >
                {renderField('name', 'Nome')}
                {renderField('cnpj', 'CNPJ')}
                {renderField('address', 'Endereço')}
                {renderField('email', 'E-mail')}
                {renderField('site', 'Site')}
                {renderField('phone', 'Telefone')}
                <button type="submit">{companyId ? 'Atualizar' : 'Salvar'}</button>
            </form>
        </div>
    );
};

export default CompanyDetails;
