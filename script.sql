-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- productos.dbo.Cliente definition

-- Drop table

-- DROP TABLE productos.dbo.Cliente;

CREATE TABLE productos.dbo.Cliente (
	id int IDENTITY(1,1) NOT NULL,
	nombres varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	apellidos varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	cedula varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	username varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	password nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	email nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	telefono varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__Cliente__3213E83F7BFCA860 PRIMARY KEY (id)
);


-- productos.dbo.Producto definition

-- Drop table

-- DROP TABLE productos.dbo.Producto;

CREATE TABLE productos.dbo.Producto (
	id int IDENTITY(1,1) NOT NULL,
	nombre varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	fecha_creacion datetime NULL,
	fecha_vencimiento datetime NULL,
	peso decimal(18,0) NULL,
	precio decimal(18,0) NULL,
	estado bit NULL,
	stock bit NULL,
	imagen text COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	datos_auditoria nvarchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK__Producto__3213E83F2563727B PRIMARY KEY (id)
);


