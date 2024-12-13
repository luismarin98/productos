-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- productos.dbo.Productos definition

-- Drop table

-- DROP TABLE productos.dbo.Productos;

CREATE TABLE productos.dbo.Productos (
	Id int IDENTITY(1,1) NOT NULL,
	precio decimal(18,2) NOT NULL,
	nombre nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	descripcion nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	estado bit NOT NULL,
	stock int NOT NULL,
	imagen nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	datosAuditoria nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Productos PRIMARY KEY (Id)
);


-- productos.dbo.Usuarios definition

-- Drop table

-- DROP TABLE productos.dbo.Usuarios;

CREATE TABLE productos.dbo.Usuarios (
	Id int IDENTITY(1,1) NOT NULL,
	NombreUsuario nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Nombre nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	Password nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Role] nvarchar(MAX) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT PK_Usuarios PRIMARY KEY (Id)
);


