UniPortal 🎓 - Modern University Management System
A sleek, responsive, and theme-aware school management dashboard built with ASP.NET Core MVC and Tailwind CSS.

✨ About The Project
UniPortal is a comprehensive and modern university management system designed to provide an intuitive dashboard for managing key academic entities like instructors, trainees, courses, and results. The user interface is built from the ground up with a focus on clean design and a great user experience, featuring full support for both light and dark themes.

This project serves as a practical example of building a modern web application using the ASP.NET Core MVC framework for the backend and the utility-first Tailwind CSS framework for a responsive and customizable frontend.

🚀 Key Features
Modern Dashboard: A clean, user-friendly interface for managing all aspects of the system.

Full CRUD Functionality: Easily Create, Read, Update, and Delete records for instructors, trainees, and courses.

Responsive Design: A seamless experience on all devices, from large desktops to small mobile phones.

Light & Dark Mode: Built-in theme switcher to suit user preferences and reduce eye strain.

Server-Side Pagination: Efficiently handles large datasets using X.PagedList to ensure fast load times.

Dynamic UI Components: Interactive elements and icons powered by Lucide Icons.

💻 Built With
This project leverages a modern tech stack to deliver a robust and maintainable application.

Backend:

C#

ASP.NET Core MVC

Frontend:

Tailwind CSS

Lucide Icons

Libraries:

X.PagedList for pagination.

🔧 Getting Started
To get a local copy up and running, follow these simple steps.

Prerequisites
Make sure you have the following software installed on your machine:

.NET 8 SDK (or newer)

A code editor like Visual Studio 2022 or Visual Studio Code

Git

Installation & Setup
Clone the repository:

git clone [https://github.com/Ommmarr111/UniPortal.git](https://github.com/Ommmarr111/UniPortal.git)

Navigate to the project directory:

cd UniPortal

Restore Dependencies:
Open the solution file (UniPortal.sln) in Visual Studio. It should automatically restore the required NuGet packages. If not, open the NuGet Package Manager Console and run:

dotnet restore

Run the Tailwind CSS CLI (Important!):
To compile the Tailwind CSS classes used in the views, you need to run the Tailwind CLI build process. Open a new terminal in the project's root directory and run this command. Keep this terminal running while you are developing.

npx tailwindcss -i ./wwwroot/css/site.css -o ./wwwroot/css/styles.css --watch

Run the Application:
Press F5 or the "Run" button in Visual Studio to build and launch the project. Your new UniPortal dashboard will open in your default browser!

🤝 Contributing
Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are greatly appreciated.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".

Fork the Project

Create your Feature Branch (git checkout -b feature/AmazingFeature)

Commit your Changes (git commit -m 'Add some AmazingFeature')

Push to the Branch (git push origin feature/AmazingFeature)

Open a Pull Request

📄 License
Distributed under the MIT License. See LICENSE.txt for more information.
