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

cd MedTexClinic
dotnet restore
3. Database setup
Create a new SQL database for the project.
Update the appsettings.json file with your database connection string.
4. Apply migrations
If the project uses Entity Framework Core for database management, apply the migrations to set up the database schema.


dotnet ef database update
5. Run the application
To start the API, use the following command:


dotnet run
The API should now be available at http://localhost:5000 or any other port specified in the configuration.

📝 API Endpoints
The MedTexClinic API exposes the following endpoints:

POST /api/patients – Create a new patient.
GET /api/patients – Retrieve all patients.
POST /api/doctors – Create a new doctor.
GET /api/doctors – Retrieve all doctors.
GET /api/patients/{id} – Retrieve a specific patient by ID.
GET /api/doctors/{id} – Retrieve a specific doctor by ID.
POST /api/appointments – Book an appointment for a patient.
GET /api/appointments – View all appointments.
GET /api/appointments/{id} – View a specific appointment by ID.

🧪 Testing the API
You can test the API using Postman or a similar API testing tool. Send HTTP requests to the endpoints and verify the responses.

🤝 Contributing
We welcome contributions to MedTexClinic API! If you'd like to contribute:

Fork the repository.
Create a new branch.
Make your changes and test them.
Submit a pull request with a description of the changes.
Please make sure to follow the coding standards and add unit tests for new features.

📄 License
This project is licensed under the Apache License 2.0. See the LICENSE file for details.

📬 Contact
For any questions or inquiries, feel free to contact the project maintainer:

mammadliayxan0@gmail.com
