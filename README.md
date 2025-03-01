<<<<<<< HEAD
# Technical Test: HR Medical Records Management System

## Objective
Develop a medical records management system for the HR department by implementing a RESTful API that allows management of employee medical records.

## Mandatory Technical Requirements
- C# .NET 8 
- PostgreSQL **(local environment)**
- Entity Framework Core (Database First, Fluent API)
- AutoMapper
- Repository Pattern
- Service Pattern
- Swagger for API documentation
- FluentValidation

## Database Structure

### Database Name: **RRHH_DB**

### Table: T_MEDICAL_RECORD
```sql
CREATE TABLE T_MEDICAL_RECORD (
    MEDICAL_RECORD_ID SERIAL PRIMARY KEY,
    FILE_ID INTEGER, -- FILE_ID represents the person to whom the MEDICAL_RECORD belongs, but it is not found in the database.
    AUDIOMETRY VARCHAR(2),
    POSITION_CHANGE VARCHAR(2),
    MOTHER_DATA VARCHAR(2000),
    DIAGNOSIS VARCHAR(100),
    OTHER_FAMILY_DATA VARCHAR(2000),
    FATHER_DATA VARCHAR(2000),
    EXECUTE_MICROS VARCHAR(2),
    EXECUTE_EXTRA VARCHAR(2),
    VOICE_EVALUATION VARCHAR(2),
    DELETION_DATE DATE,
    CREATION_DATE DATE,
    MODIFICATION_DATE DATE,
    END_DATE DATE,
    START_DATE DATE,
    STATUS_ID INTEGER,
    MEDICAL_RECORD_TYPE_ID INTEGER,
    DISABILITY VARCHAR(2),
    MEDICAL_BOARD VARCHAR(200),
    DELETION_REASON VARCHAR(2000),
    OBSERVATIONS VARCHAR(2000),
    DISABILITY_PERCENTAGE NUMERIC(10),
    DELETED_BY VARCHAR(2000),
    CREATED_BY VARCHAR(2000),
    MODIFIED_BY VARCHAR(2000),
    AREA_CHANGE VARCHAR(2)
);

CREATE TABLE STATUS (
    STATUS_ID SERIAL PRIMARY KEY,
    NAME VARCHAR(100),
    DESCRIPTION VARCHAR(500)
);

CREATE TABLE MEDICAL_RECORD_TYPE (
    MEDICAL_RECORD_TYPE_ID SERIAL PRIMARY KEY,
    NAME VARCHAR(100),
    DESCRIPTION VARCHAR(500)
);

ALTER TABLE T_MEDICAL_RECORD
ADD CONSTRAINT FK_STATUS_ID_RECORD
FOREIGN KEY (STATUS_ID) REFERENCES STATUS(STATUS_ID);

ALTER TABLE T_MEDICAL_RECORD
ADD CONSTRAINT FK_MEDICAL_RECORD_TYPE
FOREIGN KEY (MEDICAL_RECORD_TYPE_ID) REFERENCES MEDICAL_RECORD_TYPE(MEDICAL_RECORD_TYPE_ID);

-- Initial test data
INSERT INTO STATUS (NAME, DESCRIPTION) VALUES 
('Active', 'Active medical record'),
('Inactive', 'Inactive medical record');

INSERT INTO MEDICAL_RECORD_TYPE (NAME, DESCRIPTION) VALUES 
('Regular', 'Regular medical record'),
('Special', 'Special medical record');
```

## Functional Requirements

### 1. Endpoints to Implement
- **GetFilterMedicalRecords**: List of medical records with pagination and filters. It should be possible to filter by STATUS_ID (optional), START_DATE (optional), END_DATE (optional), and MEDICAL_RECORD_TYPE_ID (optional).
    Page and PageSize are mandatory parameters.
- **GetMedicalRecordById**: Retrieve medical record by ID. Get a more detailed and formatted description of the medical record.
- **AddMedicalRecord**: Create new medical record. Mandatory fields must be validated and comply with validation rules.
- **UpdateMedicalRecord**: Update existing medical record. Mandatory fields must be validated and comply with validation rules.
- **DeleteMedicalRecord**: Delete medical record (logical deletion)

### 2. Required Validations

#### 2.1 Date Controls
- START_DATE cannot be later than END_DATE
- START_DATE cannot be a future date
- If END_DATE exists, it must be later than START_DATE
- CREATION_DATE is mandatory and must be auto-generated when inserting the record

#### 2.2 Required Fields
The following fields are mandatory:
- DIAGNOSIS
- START_DATE
- STATUS_ID
- MEDICAL_RECORD_TYPE_ID
- FILE_ID
- CREATED_BY

#### 2.3 Related Records Validation
- STATUS_ID must exist in the STATUS table
- MEDICAL_RECORD_TYPE_ID must exist in the MEDICAL_RECORD_TYPE table
- A record cannot be deleted without providing DELETION_REASON
- When creating or modifying a record, the status must be valid according to these rules:
  * Cannot assign 'Inactive' status when creating a new record
  * To change to 'Inactive' status, DELETION_REASON must be provided

#### 2.4 Maximum Length Validation
- DIAGNOSIS: maximum 100 characters
- MOTHER_DATA: maximum 2000 characters
- FATHER_DATA: maximum 2000 characters
- OTHER_FAMILY_DATA: maximum 2000 characters
- MEDICAL_BOARD: maximum 200 characters
- DELETION_REASON: maximum 2000 characters
- OBSERVATIONS: maximum 2000 characters
- Two-character fields (must be 'YES' or 'NO'):
  * AUDIOMETRY
  * POSITION_CHANGE
  * EXECUTE_MICROS
  * EXECUTE_EXTRA
  * VOICE_EVALUATION
  * DISABILITY
  * AREA_CHANGE

#### 2.5 Valid Status Control
Allowed statuses and their rules:
1. Active (ID: 1)
   - Default initial status for new records
   - Allows modification of all fields
   - Requires CREATED_BY

2. Inactive (ID: 2)
   - Requires DELETION_REASON
   - Requires END_DATE
   - Requires DELETED_BY
   - Does not allow subsequent modifications
   - Must record DELETION_DATE

#### 2.6 Additional Validation Rules
- DISABILITY_PERCENTAGE must be between 0 and 100 when DISABILITY = 'YES'
- If POSITION_CHANGE = 'YES', OBSERVATIONS field is mandatory
- If END_DATE exists, the record must change to Inactive status
- CREATED_BY, MODIFIED_BY, and DELETED_BY must record the user performing the operation
- MODIFICATION_DATE must be automatically updated when modifying any field

### 3. Response Handling

#### 3.1 Base Response Model
=======
# Prueba Técnica: Sistema de Gestión de Archivos Médicos RRHH

## Objetivo
Desarrollar un sistema de gestión de archivos médicos para el departamento de RRHH, implementando una API RESTful que permita gestionar los registros médicos de los empleados.

## Requisitos Técnicos Obligatorios
- .NET 8
- PostgreSQL **(entorno local)**
- Entity Framework Core (Database first, Fluent API)
- AutoMapper
- Patrón Repository
- Patrón Service
- Swagger para documentación de API
- FluentValidation

## Estructura de Base de Datos

### Database Name: **RRHH_DB**

### Tabla: T_ARCHIVO_MEDICO
```sql
CREATE TABLE T_ARCHIVO_MEDICO (
    ID_ARCHIVO_MEDICO SERIAL PRIMARY KEY,
    AUDIOMETRIA VARCHAR(2),
    CAMBIO_PUESTO VARCHAR(2),
    DATOS_MADRE VARCHAR(2000),
    DIAGNOSTICO VARCHAR(100),
    DATOS_OTRO_FAMILIAR VARCHAR(2000),
    DATOS_PADRE VARCHAR(2000),
    EJECTUAR_MICROS VARCHAR(2),
    EJECUTAR_EXTRA VARCHAR(2),
    EVALUACION_VOZ VARCHAR(2),
    FEC_BAJA DATE,
    FEC_ING DATE,
    FEC_MOD DATE,
    FECHA_FIN DATE,
    FECHA_INICIO DATE,
    ID_ESTADO INTEGER,
    ID_TIPO_ARCHIVO_MEDICO INTEGER,
    INCAPACIDAD VARCHAR(2),
    JUNTA_MEDICA VARCHAR(200),
    MOTIVO_BAJA VARCHAR(2000),
    OBSERVACIONES VARCHAR(2000),
    PORCENTAJE_INCAP NUMERIC(10),
    USER_BAJA VARCHAR(2000),
    USER_ING VARCHAR(2000),
    USER_MOD VARCHAR(2000),
    CAMBIO_AREA VARCHAR(2)
);

CREATE TABLE ESTADO (
    ID_ESTADO SERIAL PRIMARY KEY,
    NOMBRE VARCHAR(100),
    DESCRIPCION VARCHAR(500)
);

CREATE TABLE TIPO_ARCHIVO_MEDICO (
    ID_TIPO_ARCHIVO_MEDICO SERIAL PRIMARY KEY,
    NOMBRE VARCHAR(100),
    DESCRIPCION VARCHAR(500)
);

ALTER TABLE T_ARCHIVO_MEDICO
ADD CONSTRAINT FK_ID_ESTADO_ARCHIVO
FOREIGN KEY (ID_ESTADO) REFERENCES ESTADO(ID_ESTADO);

ALTER TABLE T_ARCHIVO_MEDICO
ADD CONSTRAINT FK_ID_TIPO_ARCHIVO
FOREIGN KEY (ID_TIPO_ARCHIVO_MEDICO) REFERENCES TIPO_ARCHIVO_MEDICO(ID_TIPO_ARCHIVO_MEDICO);

-- Datos iniciales para pruebas
INSERT INTO ESTADO (NOMBRE, DESCRIPCION) VALUES 
('Activo', 'Archivo médico activo'),
('Inactivo', 'Archivo médico inactivo');

INSERT INTO TIPO_ARCHIVO_MEDICO (NOMBRE, DESCRIPCION) VALUES 
('Regular', 'Archivo médico regular'),
('Especial', 'Archivo médico especial');
```

## Requerimientos Funcionales

### 1. Endpoints a Implementar
- **GetFilterArchivosMedicos**: Listado de archivos médicos con paginación y filtros. Se debera poder filtrar por ID_ESTADO (opcional), FECHA_INICIO (opcional), FECHA_FIN (opcional) y ID_TIPO_ARCHIVO_MEDICO (opcional).
    Page y PageSize son parámetros obligatorios.
- **GetArchivMedicoById**: Obtener archivo médico por ID. Obtener una descripcion mas detallada y formateada del archivo médico.
- **AddArchivoMedico**: Crear nuevo archivo médico. Se deberá validar que los campos obligatorios estén presentes y que cumplan con las reglas de validación.
- **UpdateArchivoMedico**:Actualizar archivo médico existente. Se deberá validar que los campos obligatorios estén presentes y que cumplan con las reglas de validación.
- **DeleteArchivoMedico**: Eliminar archivo médico (baja lógica)

### 2. Validaciones Requeridas

#### 2.1 Control de Fechas
- La FECHA_INICIO no puede ser mayor a FECHA_FIN
- La FECHA_INICIO no puede ser una fecha futura
- Si existe FECHA_FIN, debe ser mayor a FECHA_INICIO
- FEC_ING (Fecha de Ingreso) es obligatoria y debe ser autogenerada al insertar el dato

#### 2.2 Campos Requeridos
Los siguientes campos son obligatorios:
- DIAGNOSTICO
- FECHA_INICIO
- ID_ESTADO
- ID_TIPO_ARCHIVO_MEDICO
- ID_LEGAJO
- USER_ING

#### 2.3 Validación de Registros Relacionados
- ID_ESTADO debe existir en la tabla ESTADO
- ID_TIPO_ARCHIVO_MEDICO debe existir en la tabla TIPO_ARCHIVO_MEDICO
- No se puede dar de baja un registro si no se proporciona MOTIVO_BAJA
- Al crear o modificar un registro, el estado debe ser válido según las siguientes reglas:
  * No se puede asignar un estado 'Inactivo' al crear un nuevo registro
  * Para cambiar a estado 'Inactivo' debe proporcionarse MOTIVO_BAJA

#### 2.4 Validación de Longitud Máxima
- DIAGNOSTICO: máximo 100 caracteres
- DATOS_MADRE: máximo 2000 caracteres
- DATOS_PADRE: máximo 2000 caracteres
- DATOS_OTRO_FAMILIAR: máximo 2000 caracteres
- JUNTA_MEDICA: máximo 200 caracteres
- MOTIVO_BAJA: máximo 2000 caracteres
- OBSERVACIONES: máximo 2000 caracteres
- Campos de dos caracteres (deben ser 'SI' o 'NO'):
  * AUDIOMETRIA
  * CAMBIO_PUESTO
  * EJECTUAR_MICROS
  * EJECUTAR_EXTRA
  * EVALUACION_VOZ
  * INCAPACIDAD
  * CAMBIO_AREA

#### 2.5 Control de Estados Válidos
Estados permitidos y sus reglas:
1. Activo (ID: 1)
   - Estado inicial por defecto para nuevos registros
   - Permite modificación de todos los campos
   - Requiere USER_ING

2. Inactivo (ID: 2)
   - Requiere MOTIVO_BAJA
   - Requiere FECHA_FIN
   - Requiere USER_BAJA
   - No permite modificaciones posteriores
   - Debe registrar FEC_BAJA

#### 2.6 Reglas Adicionales de Validación
- PORCENTAJE_INCAP debe estar entre 0 y 100 cuando INCAPACIDAD = 'SI'
- Si CAMBIO_PUESTO = 'SI', el campo OBSERVACIONES es obligatorio
- Si existe FECHA_FIN, el registro debe pasar a estado Inactivo
- Los campos USER_ING, USER_MOD y USER_BAJA deben registrar el usuario que realiza la operación
- FEC_MOD debe actualizarse automáticamente al modificar cualquier campo

### 3. Manejo de Respuestas

#### 3.1 Modelo de Respuesta Base
>>>>>>> 572aaf9b82d49cd8c386f576e0dc46aff71f645c
```csharp
public class BaseResponse<T>
{
    public bool? Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public int? Code { get; set; }
    public int? TotalRows { get; set; }
    public string? Exception { get; set; }
}
```

<<<<<<< HEAD
#### 3.2 HTTP Response Codes
- 200 OK: Successful request (GET, PUT/PATCH)
- 400 Bad Request: Validation errors
- 404 Not Found: Resource not found
- 500 Internal Server Error: Unhandled errors

## Deliverables
1. Git repository with complete source code
2. Installation and execution instructions in README

## Evaluation Criteria
- Code structure and organization
- Correct implementation of requested patterns
- Validation and exception handling
- Proper use of AutoMapper
- Clean and properly commented code
- Correct application of Gitflow (Main -> Development -> Feature)

## Delivery Time
- 5 business days from test receipt
- Delivery will be confirmed through creation of a Release PR to main

## Installation Instructions
The candidate must provide clear instructions for:
1. Local PostgreSQL installation
2. Database script execution
3. Running migration with database-first approach using EF with Fluent API
4. Project configuration

## Extras (Not Mandatory)
- Docker

## Delivery Format
- Git repository (GitHub, GitLab, Bitbucket)
- Repository must include this README updated with specific installation instructions for your implementation

---
*Note: For any questions or clarifications about requirements, please create an issue in the repository.*
=======
#### 3.2 Códigos de Respuesta HTTP
- 200 OK: Petición exitosa (GET, PUT/PATCH)
- 400 Bad Request: Errores de validación
- 404 Not Found: Recurso no encontrado
- 500 Internal Server Error: Errores no controlados

## Entregables
1. Repositorio Git con el código fuente completo
2. Instrucciones de instalación y ejecución en el README

## Criterios de Evaluación
- Estructura y organización del código
- Implementación correcta de patrones solicitados
- Manejo de validaciones y excepciones
- Uso adecuado de AutoMapper
- Código limpio y comentado adecuadamente
- Correcta aplicacion de Gitflow (Main -> Development -> Feat)

## Tiempo de Entrega
- 5 días hábiles desde la recepción de la prueba
- La entrega se confimara mediante la creacion de una PR Release a main

## Instrucciones de Instalación
El candidato debe proporcionar instrucciones claras para:
1. Instalación de PostgreSQL local
2. Ejecución del script de base de datos
3. Ejecutar la migracion con el enfoque database first utilizando EF con fluent api
4. Configuración del proyecto 

## Extras (No Obligatorios)
- Docker

## Formato de Entrega
- Repositorio Git (GitHub, GitLab, Bitbucket)
- El repositorio debe incluir este README actualizado con las instrucciones de instalación específicas de tu implementación

---
*Nota: Para cualquier duda o aclaración sobre los requerimientos, por favor crear un issue en el repositorio.*
>>>>>>> 572aaf9b82d49cd8c386f576e0dc46aff71f645c
