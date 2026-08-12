
create table Artistas(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	nome varchar(100) not null,
	descricao varchar(500) not null
);



create table Locais(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	nome varchar(100) not null,
	capacidade int not null,
	rua varchar(200) null,
	bairro varchar(200) null,
	cidade varchar(200) null,
	uf varchar(2) null,
	pais varchar(100) null,
	cep varchar(10) null
);


create table Shows(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	nome varchar(100) not null,
	descricao varchar(500) null,
	artista bigint references artistas(id),
	local bigint references locais(id),
	data date not null
);


create table Clientes(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	nome varchar(200) not null,
	usuario varchar(200) not null,
	senha varchar(200) not null,
	email varchar(200) not null,
	telefone varchar(15) null,
	rua varchar(200) null,
	bairro varchar(200) null,
	cidade varchar(200) null,
	uf varchar(2) null,
	pais varchar(100) null,
	cep varchar(10) null
	
);

create table Ingressos(
	id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
	data date not null,
	cliente bigint references clientes(id),
	codigo varchar(50),
	setor varchar(50),
	show bigint references shows(id)	
);



-- Inserções na tabela Artista
INSERT INTO Artistas (nome, descricao) VALUES
('Os Mutantes', 'Banda brasileira de rock psicodélico formada durante o movimento Tropicalista.'),
('Caetano Veloso', 'Cantor, compositor, produtor e escritor brasileiro de renome internacional.'),
('Liniker', 'Cantora, compositora e atriz brasileira com influências de Soul, R&B e MPB.'),
('Sepultura', 'Banda pioneira de heavy metal e thrash metal fundada em Belo Horizonte.'),
('Djavan', 'Ícone da MPB com misturas de ritmos tradicionais, samba e jazz.');

-- Inserções na tabela Local
INSERT INTO Locais (nome, capacidade, rua, bairro, cidade, uf, pais, cep) VALUES
('Allianz Parque', 45000, 'Av. Francisco Matarazzo, 1705', 'Água Branca', 'São Paulo', 'SP', 'Brasil', '05001-200'),
('Circo Voador', 2500, 'Rua dos Arcos, s/n', 'Lapa', 'Rio de Janeiro', 'RJ', 'Brasil', '20031-040'),
('Audio Club', 3200, 'Av. Francisco Matarazzo, 694', 'Barra Funda', 'São Paulo', 'SP', 'Brasil', '05001-000'),
('Ópera de Arame', 1572, 'Rua João Gava, 970', 'Abranches', 'Curitiba', 'PR', 'Brasil', '82130-010'),
('Espaço Unimed', 8000, 'Rua Tagipuru, 795', 'Barra Funda', 'São Paulo', 'SP', 'Brasil', '01156-000');

-- Inserções na tabela Show
-- Observação: Assume-se que os IDs gerados para Artista e Local correspondam à sequência 1 a 5
INSERT INTO Shows (nome, descricao, artista, local, data) VALUES
('Turnê Transversal', 'Apresentação comemorativa dos maiores sucessos de carreira.', 2, 1, '2026-08-11'),
('Noite do Heavy Metal', 'Show especial comemorativo de encerramento de turnê mundial.', 4, 1, '2026-09-11'),
('Índigo Borboleta Anil', 'Apresentação intimista com repertório autoral e participações.', 3, 2, '2026-08-11'),
('Voz e Violão no Teatro', 'Concerto acústico clássico.', 5, 4, '2026-10-11'),
('Psicodelia Viva', 'Show histórico reunindo clássicos dos anos 60 e 70.', 1, 3, '2026-10-11');

-- Inserções na tabela Cliente
INSERT INTO Clientes (nome, usuario, senha, email, telefone, rua, bairro, cidade, uf, pais, cep) VALUES
('Mariana Souza', 'mari.souza', '$2a$12$e8Y...hash1', 'mariana.souza@email.com', '11987654321', 'Rua das Flores, 123', 'Pinheiros', 'São Paulo', 'SP', 'Brasil', '05410-010'),
('Carlos Eduardo Lima', 'cadu.lima', '$2a$12$f9Z...hash2', 'carlos.lima@email.com', '21976543210', 'Av. Atlântica, 450', 'Copacabana', 'Rio de Janeiro', 'RJ', 'Brasil', '22070-000'),
('Beatriz Ferreira', 'bia_ferreira', '$2a$12$g1A...hash3', 'bia.ferreira@email.com', '41991234567', 'Rua Marechal Deodoro, 800', 'Centro', 'Curitiba', 'PR', 'Brasil', '80010-010'),
('Lucas Mendes', 'lucas.mendes', '$2a$12$h2B...hash4', 'lucas.m@email.com', '31988776655', 'Rua da Bahia, 1020', 'Lourdes', 'Belo Horizonte', 'MG', 'Brasil', '30160-011'),
('Fernanda Rocha', 'fernandarocha', '$2a$12$i3C...hash5', 'f.rocha@email.com', '11965432109', 'Rua Augusta, 2100', 'Consolação', 'São Paulo', 'SP', 'Brasil', '01412-000');

-- Inserções na tabela Ingresso
INSERT INTO Ingressos (data, cliente, codigo, setor, show) VALUES
('2026-09-15', 1, 'ING-2026-TRN-001', 'Pista Premium', 1),
('2026-09-15', 2, 'ING-2026-TRN-002', 'Cadeira Inferior', 1),
('2026-10-02', 4, 'ING-2026-MTL-003', 'Pista', 2),
('2026-10-20', 2, 'ING-2026-IBA-004', 'Pista Geral', 3),
('2026-10-20', 3, 'ING-2026-IBA-005', 'Mezanino', 3),
('2026-11-10', 3, 'ING-2026-ACU-006', 'Plateia Central', 4),
('2026-11-10', 5, 'ING-2026-ACU-007', 'Camarote', 4),
('2026-12-05', 1, 'ING-2026-PSC-008', 'Pista', 5),
('2026-12-05', 5, 'ING-2026-PSC-009', 'Área VIP', 5);




