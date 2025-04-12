# Travel Site Web Application

**EventManagementWebApp** is a comprehensive web application designed to facilitate event management. It enables users to create, edit, view, and register for various events, providing an intuitive interface for both organizers and participants.

## 🧩 Features

- **Create and Edit Events**  
  Organizers can add new events by specifying details such as name, date, location, and description.
- **View Events**  
  Users have access to a list of available events with filtering and sorting options.
- **Register for Events**  
  Participants can register for selected events, with the system managing seat availability.
- **Authentication & Authorization**  
  Role-based access (organizer vs user) using ASP.NET Identity.
- **Administrative Panel**  
  Available for organizers to manage events and participants.
- **Database Integration**  
  Uses Entity Framework Core with a SQL Server database for persistent data storage.

## 🛠️ Technologies Used

- **Frontend**: ASP.NET Core MVC, Razor Pages, HTML5, CSS3, JavaScript
- **Backend**: ASP.NET Core, C#
- **Database**: Microsoft SQL Server
- **Other**: Entity Framework Core for ORM, Identity for authentication and authorization

## 🚀 Running the Project Locally

1. **Clone the repository:**
   ```bash
   git clone https://github.com/arturzelek1/EventManagementWebApp.git
   cd EventManagementWebApp
2. **Set up database:**
   ```bash
   dotnet ef database update
3. **Run the application:**
   ```bash
   dotnet run

## 📁 Project Structure

```plaintext
EventManagementWebApp/
├── Controllers/              # MVC Controllers for routing and logic
│   ├── EventController.cs
│   └── AccountController.cs
├── Models/                   # Entity models used with EF Core
│   ├── Event.cs
│   └── ApplicationUser.cs
├── Views/                    # Razor views (UI)
│   ├── Event/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   └── Register.cshtml
│   └── Shared/
├── Data/                     # DbContext and migrations
│   ├── ApplicationDbContext.cs
├── wwwroot/                  # Static assets (CSS, JS, images)
│   └── css/site.css
├── appsettings.json          # Configuration file
├── Program.cs                # Application entry point
├── Startup.cs                # Middleware and services configuration
├── EventManagementWebApp.csproj
└── EventManagementWebApp.sln
