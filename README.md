# MedTexClinic API

**MedTexClinic API** is a backend solution designed to handle all aspects of patient management, appointment scheduling, and billing for healthcare clinics. This project is built using **.NET Core** and **SQL Server**, providing a robust and scalable API for clinic management.

## 🚀 Features

- **Patient Management:** Securely store, update, and retrieve patient information.
- **Appointment Scheduling:** Allow patients to book, reschedule, and cancel appointments with doctors.
- **Doctor's Dashboard:** Provides doctors with the ability to manage their appointments and patient records.
- **Admin Panel Access:** Administrative users can oversee all operations, including staff management and billing records.
- **Notification System:** Automatically notify patients and doctors about upcoming appointments and changes.

## 🛠️ Prerequisites

Before you begin, ensure that you have the following installed on your machine:

- **.NET Core SDK** (v5.0 or later)
- **SQL Server** (or any compatible SQL database)
- **Postman** or a similar API testing tool for testing the API endpoints
- **Visual Studio** or any preferred code editor

## 📥 Installation

Follow these steps to set up the MedTexClinic API project locally:

### 1. Clone the repository


git clone https://github.com/AyxanMammadlidev/MedTexClinic.git


2. Restore the project dependencies
Navigate to the project folder and restore the dependencies:

cd MedTexClinic<br>
dotnet restore<br>
3. Database setup<br>
Create a new SQL database for the project.<br>
Update the appsettings.json file with your database connection string.<br>
4. Apply migrations<br>
If the project uses Entity Framework Core for database management, apply the migrations to set up the database schema.


dotnet ef database update<br>
5. Run the application<br>
To start the API, use the following command:


dotnet run<br>
The API should now be available at http://localhost:5000 or any other port specified in the configuration.

📝 API Endpoints
The MedTexClinic API exposes the following endpoints:

POST /api/patients – Create a new patient.<br>
GET /api/patients – Retrieve all patients.<br>
POST /api/doctors – Create a new doctor.<br>
GET /api/doctors – Retrieve all doctors.<br>
GET /api/patients/{id} – Retrieve a specific patient by ID.<br>
GET /api/doctors/{id} – Retrieve a specific doctor by ID.<br>
POST /api/appointments – Book an appointment for a patient.<br>
GET /api/appointments – View all appointments.<br>
GET /api/appointments/{id} – View a specific appointment by ID.<br>

🧪 Testing the API
You can test the API using Postman or a similar API testing tool. Send HTTP requests to the endpoints and verify the responses.

🤝 Contributing
We welcome contributions to MedTexClinic API! If you'd like to contribute:

Fork the repository.<br>
Create a new branch.<br>
Make your changes and test them.<br>
Submit a pull request with a description of the changes.<br>
Please make sure to follow the coding standards and add unit tests for new features.<br>

📄 License
This project is licensed under the Apache License 2.0. See the LICENSE file for details.

📬 Contact
For any questions or inquiries, feel free to contact the project maintainer:

memmedliayxan0@gmail.com
