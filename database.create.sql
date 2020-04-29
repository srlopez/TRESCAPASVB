DROP TABLE IF EXISTS USUARIOS;
DROP TABLE IF EXISTS MASCOTAS;

CREATE TABLE USUARIOS(
 ID     INTEGER PRIMARY KEY NOT NULL,
 NOMBRE VARCHAR(50) NOT NULL,
 FNAC   CHAR(8),
 EMAIL  VARCHAR(255),
 MAP    DECIMAL(3,2),
 NOTAS  TEXT
);

CREATE TABLE MASCOTAS(
 ID            INTEGER PRIMARY KEY NOT NULL,
 IDPROPIETARIO INT NOT NULL,
 NOMBRE        VARCHAR(50) NOT NULL,
 FNAC          CHAR(8),
 FOREIGN KEY(IDPROPIETARIO) REFERENCES USUARIOS(ID)
);

.tables

INSERT INTO USUARIOS 
(NOMBRE, FNAC, EMAIL, MAP) VALUES 
("USUARIO#1", "16072000", "us1@email.com", 12.34);

INSERT INTO MASCOTAS
(IDPROPIETARIO, NOMBRE, FNAC) VALUES
(1, "TIGGER", "01012020");

SELECT * 
FROM USUARIOS U INNER JOIN MASCOTAS M 
ON U.ID=M.IDPROPIETARIO;


