-- Criar extensão pgcrypto antes de qualquer tabela que use gen_random_uuid()
CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- Tabela customer
CREATE TABLE customer (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    name varchar(100) NOT NULL,
    email varchar(100) NOT NULL,
    documentnumber varchar(11) NOT NULL,
    phonenumber varchar(11) NOT NULL,
    address varchar(150) NOT NULL,
    city varchar(50) NOT NULL,
    state varchar(2) NOT NULL,
    country varchar(20) NOT NULL,
    postalcode varchar(8) NOT NULL,
    createdat TIMESTAMP NOT NULL,
    updatedat TIMESTAMP NULL,
    isactive boolean NOT NULL
);

-- Índices únicos para customer
CREATE UNIQUE INDEX idx_customer_email ON customer (email);
CREATE UNIQUE INDEX idx_customer_documentnumber ON customer (documentnumber);

-- Tabela appuser
CREATE TABLE appuser (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    name varchar(100) NOT NULL,
    email varchar(100) NOT NULL,
    login varchar(50) NOT NULL,
    password varchar(255) NOT NULL,
    role INT NOT NULL, 
    createdat TIMESTAMP NOT NULL,
    updatedat TIMESTAMP NULL,
    isactive boolean NOT NULL
);

-- Índices únicos para appuser
CREATE UNIQUE INDEX idx_appuser_email ON appuser (email);
CREATE UNIQUE INDEX idx_appuser_login ON appuser (login);

