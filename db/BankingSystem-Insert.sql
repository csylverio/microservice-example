INSERT INTO Customer (
    Id, Name, Email, DocumentNumber, PhoneNumber, Address, City, State, Country, PostalCode, CreatedAt, UpdatedAt, IsActive
) VALUES 
(
    gen_random_uuid(), 
    'João Silva', 
    'joao.silva@example.com', 
    '12345678901', 
    '11987654321', 
    'Rua das Flores, 123', 
    'São Paulo', 
    'SP', 
    'Brasil', 
    '12345678', 
    CURRENT_TIMESTAMP, 
    NULL, 
    true
),
(
    gen_random_uuid(), 
    'Maria Oliveira', 
    'maria.oliveira@example.com', 
    '98765432100', 
    '21987654321', 
    'Av. Paulista, 456', 
    'Rio de Janeiro', 
    'RJ', 
    'Brasil', 
    '87654321', 
    CURRENT_TIMESTAMP, 
    NULL, 
    true
);

INSERT INTO AppUser (
    Id, Name, Email, Login, Password, Role, CreatedAt, UpdatedAt, IsActive
) VALUES 
(
    gen_random_uuid(), 
    'Admin User', 
    'admin@example.com', 
    'admin', 
    'hashed_password_12345', 
    1, 
    CURRENT_TIMESTAMP, 
    NULL, 
    true
),
(
    gen_random_uuid(), 
    'John Doe', 
    'john.doe@example.com', 
    'johndoe', 
    'hashed_password_67890', 
    2, 
    CURRENT_TIMESTAMP, 
    NULL, 
    true
);
