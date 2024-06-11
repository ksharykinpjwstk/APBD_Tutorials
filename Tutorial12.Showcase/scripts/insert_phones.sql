-- noinspection SqlNoDataSourceInspectionForFile

INSERT INTO Phone (ModelName, CoreCount, Ram, Has5G, Description, PhoneManufactureId)
VALUES
    ('Acer Liquid Z6', 4, 2, 0, 'A mid-range smartphone by Acer', (SELECT Id FROM PhoneManufacture WHERE Name = 'Acer')),
    ('Alcatel 1S', 8, 3, 1, 'An affordable smartphone by Alcatel', (SELECT Id FROM PhoneManufacture WHERE Name = 'Alcatel Mobile')),
    ('Apple iPhone 12', 6, 4, 1, 'Flagship model by Apple', (SELECT Id FROM PhoneManufacture WHERE Name = 'Apple')),
    ('Asus ROG Phone 5', 8, 16, 1, 'Gaming smartphone by Asus', (SELECT Id FROM PhoneManufacture WHERE Name = 'Asus')),
    ('BlackBerry KEY2', 8, 6, 0, 'A smartphone with a physical keyboard by BlackBerry', (SELECT Id FROM PhoneManufacture WHERE Name = 'BlackBerry')),
    ('Google Pixel 5', 8, 8, 1, null, (SELECT Id FROM PhoneManufacture WHERE Name = 'Google')),
('Huawei P40', 8, 8, 1, 'Flagship smartphone by Huawei', (SELECT Id FROM PhoneManufacture WHERE Name = 'Huawei')),
('LG Velvet', 8, 6, 1, 'A stylish smartphone by LG', (SELECT Id FROM PhoneManufacture WHERE Name = 'LG')),
('Motorola Edge', 8, 6, 1, 'A smartphone with a curved screen by Motorola', (SELECT Id FROM PhoneManufacture WHERE Name = 'Motorola Mobility')),
('Nokia 7.2', 8, 4, 0, 'A mid-range smartphone by Nokia', (SELECT Id FROM PhoneManufacture WHERE Name = 'HMD Global')),
('OnePlus 9', 8, 8, 1, 'Flagship smartphone by OnePlus', (SELECT Id FROM PhoneManufacture WHERE Name = 'OnePlus')),
('Oppo Reno 5', 8, 8, 1, 'A stylish smartphone by Oppo', (SELECT Id FROM PhoneManufacture WHERE Name = 'Oppo'));
