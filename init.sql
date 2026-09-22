-- docker exec -i ticketeira-postgres-1 psql -U postgres -d ticketeira_db < init.sql

DROP TABLE IF EXISTS ingressos CASCADE;
DROP TABLE IF EXISTS clientes CASCADE;
DROP TABLE IF EXISTS eventos CASCADE;
DROP TABLE IF EXISTS locais CASCADE;
DROP TABLE IF EXISTS artistas CASCADE;
DROP TABLE IF EXISTS evento_artista CASCADE;

create table artistas(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	nome varchar(100) not null,
	descricao varchar(500) not null
);



create table locais(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	nome varchar(100) not null,
	capacidade int not null,
	logradouro varchar(200) null,
	bairro varchar(200) null,
	cidade varchar(200) null,
	uf varchar(2) null,
	pais varchar(100) null,
	cep varchar(10) null,
    numero varchar(10) null
);


create table eventos(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	nome varchar(100) not null,
	descricao varchar(500) null,
	artista bigint references artistas(id),
	local bigint references locais(id),
	data date not null
);


create table clientes(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	nome varchar(200) not null,
	usuario varchar(200) not null,
	senha varchar(200) not null,
	email varchar(200) not null,
	telefone varchar(15) null,
	logradouro varchar(200) null,
	bairro varchar(200) null,
	cidade varchar(200) null,
	uf varchar(2) null,
	pais varchar(100) null,
	cep varchar(10) null
	
);

create table ingressos(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	data date not null,
	cliente bigint references clientes(id),
	codigo varchar(50),
	setor varchar(50),
	evento bigint references eventos(id)	
);



CREATE TABLE evento_artista (
    evento_id INT PRIMARY KEY,
    evento_nome VARCHAR(255) NOT NULL,
    evento_descricao TEXT,
    evento_data TIMESTAMP,
    
    artista_id INT NOT NULL,
    artista_nome VARCHAR(255) NOT NULL,
    artista_descricao TEXT,
    
    local_id INT NOT NULL,
    local_nome VARCHAR(255) NOT NULL,
    local_capacidade INT,
    local_logradouro VARCHAR(255),
    local_numero VARCHAR(50),
    local_cidade VARCHAR(100),
    local_uf VARCHAR(100),
    local_pais VARCHAR(100),
    local_cep VARCHAR(20)
);



-- ============================================================================
-- 1. TRIGGER PARA A TABELA 'eventos' (INSERT, UPDATE e DELETE)
-- ============================================================================
CREATE OR REPLACE FUNCTION trg_sync_evento_artista_eventos()
RETURNS TRIGGER AS $$
BEGIN
    IF (TG_OP = 'DELETE') THEN
        DELETE FROM evento_artista WHERE evento_id = OLD.id;
        RETURN OLD;
    END IF;

    INSERT INTO evento_artista (
        evento_id, evento_nome, evento_descricao, evento_data,
        artista_id, artista_nome, artista_descricao,
        local_id, local_nome, local_capacidade, 
        local_logradouro, local_numero, local_cidade, local_uf, local_pais, local_cep
    )
    SELECT 
        s.id, s.nome, s.descricao, s.data,
        a.id, a.nome, a.descricao,
        l.id, l.nome, l.capacidade, 
        l.logradouro, l.numero, l.cidade, l.uf, l.pais, l.cep
    FROM eventos s
    INNER JOIN "artistas" a ON s.artista = a.id
    INNER JOIN "locais" l ON s.local = l.id
    WHERE s.id = NEW.id
    ON CONFLICT (evento_id) DO UPDATE SET
        evento_nome = EXCLUDED.evento_nome,
        evento_descricao = EXCLUDED.evento_descricao,
        evento_data = EXCLUDED.evento_data,
        artista_id = EXCLUDED.artista_id,
        artista_nome = EXCLUDED.artista_nome,
        artista_descricao = EXCLUDED.artista_descricao,
        local_id = EXCLUDED.local_id,
        local_nome = EXCLUDED.local_nome,
        local_capacidade = EXCLUDED.local_capacidade,
        local_logradouro = EXCLUDED.local_logradouro,
        local_numero = EXCLUDED.local_numero,
        local_cidade = EXCLUDED.local_cidade,
        local_uf = EXCLUDED.local_uf,
        local_pais = EXCLUDED.local_pais,
        local_cep = EXCLUDED.local_cep;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS tg_sync_eventos ON eventos;
CREATE TRIGGER tg_sync_eventos
AFTER INSERT OR UPDATE OR DELETE ON eventos
FOR EACH ROW EXECUTE FUNCTION trg_sync_evento_artista_eventos();


-- ============================================================================
-- 2. TRIGGER PARA A TABELA 'artistas' (UPDATE)
-- ============================================================================
CREATE OR REPLACE FUNCTION trg_sync_evento_artista_artistas()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE evento_artista
    SET 
        artista_nome = NEW.nome,
        artista_descricao = NEW.descricao
    WHERE artista_id = NEW.id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS tg_sync_artistas ON "artistas";
CREATE TRIGGER tg_sync_artistas
AFTER UPDATE ON "artistas"
FOR EACH ROW EXECUTE FUNCTION trg_sync_evento_artista_artistas();


-- ============================================================================
-- 3. TRIGGER PARA A TABELA 'locais' (UPDATE)
-- ============================================================================
CREATE OR REPLACE FUNCTION trg_sync_evento_artista_locais()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE evento_artista
    SET 
        local_nome = NEW.nome,
        local_capacidade = NEW.capacidade,
        local_logradouro = NEW.logradouro,
        local_numero = NEW.numero,
        local_cidade = NEW.cidade,
        local_uf = NEW.uf,
        local_pais = NEW.pais,
        local_cep = NEW.cep
    WHERE local_id = NEW.id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS tg_sync_locais ON "locais";
CREATE TRIGGER tg_sync_locais
AFTER UPDATE ON "locais"
FOR EACH ROW EXECUTE FUNCTION trg_sync_evento_artista_locais();



-- Inserções na tabela Artista
INSERT INTO artistas (nome, descricao) VALUES
('Os Mutantes', 'Banda brasileira de rock psicodélico formada durante o movimento Tropicalista.'),
('Caetano Veloso', 'Cantor, compositor, produtor e escritor brasileiro de renome internacional.'),
('Liniker', 'Cantora, compositora e atriz brasileira com influências de Soul, R&B e MPB.'),
('Sepultura', 'Banda pioneira de heavy metal e thrash metal fundada em Belo Horizonte.'),
('Djavan', 'Ícone da MPB com misturas de ritmos tradicionais, samba e jazz.');

-- Inserções na tabela Local
INSERT INTO locais (nome, capacidade, logradouro, bairro, cidade, uf, pais, cep) VALUES
('Allianz Parque', 45000, 'Av. Francisco Matarazzo, 1705', 'Água Branca', 'São Paulo', 'SP', 'Brasil', '05001-200'),
('Circo Voador', 2500, 'Rua dos Arcos, s/n', 'Lapa', 'Rio de Janeiro', 'RJ', 'Brasil', '20031-040'),
('Audio Club', 3200, 'Av. Francisco Matarazzo, 694', 'Barra Funda', 'São Paulo', 'SP', 'Brasil', '05001-000'),
('Ópera de Arame', 1572, 'Rua João Gava, 970', 'Abranches', 'Curitiba', 'PR', 'Brasil', '82130-010'),
('Espaço Unimed', 8000, 'Rua Tagipuru, 795', 'Barra Funda', 'São Paulo', 'SP', 'Brasil', '01156-000');

-- Inserções na tabela eventos
-- Observação: Assume-se que os IDs gerados para Artista e Local correspondam à sequência 1 a 5
INSERT INTO eventos (nome, descricao, artista, local, data) VALUES
('Turnê Transversal', 'Apresentação comemorativa dos maiores sucessos de carreira.', 2, 1, '2026-08-11'),
('Noite do Heavy Metal', 'Show especial comemorativo de encerramento de turnê mundial.', 4, 1, '2026-09-11'),
('Índigo Borboleta Anil', 'Apresentação intimista com repertório autoral e participações.', 3, 2, '2026-08-11'),
('Voz e Violão no Teatro', 'Concerto acústico clássico.', 5, 4, '2026-10-11'),
('Psicodelia Viva', 'Show histórico reunindo clássicos dos anos 60 e 70.', 1, 3, '2026-10-11');

-- Inserções na tabela Cliente
INSERT INTO clientes (nome, usuario, senha, email, telefone, logradouro, bairro, cidade, uf, pais, cep) VALUES
('Mariana Souza', 'mari.souza', '$2a$12$e8Y...hash1', 'mariana.souza@email.com', '11987654321', 'Rua das Flores, 123', 'Pinheiros', 'São Paulo', 'SP', 'Brasil', '05410-010'),
('Carlos Eduardo Lima', 'cadu.lima', '$2a$12$f9Z...hash2', 'carlos.lima@email.com', '21976543210', 'Av. Atlântica, 450', 'Copacabana', 'Rio de Janeiro', 'RJ', 'Brasil', '22070-000'),
('Beatriz Ferreira', 'bia_ferreira', '$2a$12$g1A...hash3', 'bia.ferreira@email.com', '41991234567', 'Rua Marechal Deodoro, 800', 'Centro', 'Curitiba', 'PR', 'Brasil', '80010-010'),
('Lucas Mendes', 'lucas.mendes', '$2a$12$h2B...hash4', 'lucas.m@email.com', '31988776655', 'Rua da Bahia, 1020', 'Lourdes', 'Belo Horizonte', 'MG', 'Brasil', '30160-011'),
('Fernanda Rocha', 'fernandarocha', '$2a$12$i3C...hash5', 'f.rocha@email.com', '11965432109', 'Rua Augusta, 2100', 'Consolação', 'São Paulo', 'SP', 'Brasil', '01412-000');

-- Inserções na tabela Ingresso
INSERT INTO ingressos (data, cliente, codigo, setor, evento) VALUES
('2026-09-15', 1, 'ING-2026-TRN-001', 'Pista Premium', 1),
('2026-09-15', 2, 'ING-2026-TRN-002', 'Cadeira Inferior', 1),
('2026-10-02', 4, 'ING-2026-MTL-003', 'Pista', 2),
('2026-10-20', 2, 'ING-2026-IBA-004', 'Pista Geral', 3),
('2026-10-20', 3, 'ING-2026-IBA-005', 'Mezanino', 3),
('2026-11-10', 3, 'ING-2026-ACU-006', 'Plateia Central', 4),
('2026-11-10', 5, 'ING-2026-ACU-007', 'Camarote', 4),
('2026-12-05', 1, 'ING-2026-PSC-008', 'Pista', 5),
('2026-12-05', 5, 'ING-2026-PSC-009', 'Área VIP', 5);



