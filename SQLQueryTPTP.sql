SELECT constraint_name
FROM information_schema.table_constraints
WHERE table_name = 'Productos'
AND constraint_type = 'FOREIGN KEY';


ALTER TABLE Productos DROP CONSTRAINT FK__Productos__Id_ca__70DDC3D8

DELETE FROM Productos

SELECT * FROM Compras

ALTER TABLE Productos ADD Id_categoria int,
FOREIGN KEY(Id_categoria) REFERENCES Categorias(id_categoria)

ALTER TABLE Productos DROP COLUMN Id_categoria;
