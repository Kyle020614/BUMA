This application is a basic user managment system or BUMA for short that supports user authentication when adding, registering or loging in as a user.
role-based access, and CRUD operations.

Below, you'll find instructions on how to build, run, and test the application, as well as an overview of key design decisions.

How to Build and Run the Application
    Prerequisites:
        Visual Studio.
        .NET Framework/Core 
        SQL Server: Ensure SQL Server is running and the required database schema is set up using the SQL provided.

Steps to Build and Run

    Clone the repository to your local machine using git clone [repository URL] or the built in Git interface on Visual studio. 

    Open the solution file (BUMA.sln) in Visual Studio.

    Configure the database connection:

    Update the BUMADbContext or relevant configuration file with your SQL Server details.

    Build the project:
    In Visual Studio, press Ctrl+Shift+B or select Build > Build Solution.
    Run the application:
    Press F5 to start debugging or Ctrl+F5 to run without debugging.


Design Decisions: 

Key Features
    Role-Based Access Control: Admins have access to all CRUD operations, while users have restricted permissions.

    MVVM Architecture: Ensures a clean separation of concerns between the UI and business logic.

    Data Binding: Utilizes WPF's data binding capabilities to dynamically reflect changes in the UI.

    Password Security: Passwords are hashed using MD5 before being stored in the database.

    User Dummy proofing: additional features such as validation  username uniqueness when registering adding a user
                         better error handling for if someone can't be found when logging in or if the password/username is inccorect

    Datagrid user view: Implimented a Datagrid in the UserView to allow users to populate the SelectedUser obj and set values for adding/Updating/removing records.

Challenges and Resolutions: 

    Dynamic Data Refresh: I Implemented ObservableCollections to ensure the UI updates whenever the data changes.

    Error Handling: Used exception handling to manage authentication and database operations gracefully.

    Conditional UI Behavior: Restricted editing capabilities based on user roles using IsAdmin property.

    UserView Data Binding errors: Initially, the application used text boxes to allow users to input values for the SelectedUser object. 
                                  However, data binding issues arose, preventing the text boxes from correctly populating the SelectedUser object.
                                  To try and resolve this, I transitioned to using a DataGrid, enabling users to interact directly with rows of data. 
                                  By selecting a row, the SelectedUser object is automatically populated with the corresponding data. 
                                  This approach improved both the user experience and the reliability of the data binding.

How to Test Functionality:

    Testing Scenarios:
        Creating a New User

            Launch the application.
            Navigate to the registration page.
            Fill in the username, password, and role (Admin/User) fields.
            Submit the form to create the user.

        Creating a New User ErrorHandling:
            Fill in the username, password, and role (Admin/User) fields but omit one of the input values.
            Submit the form to create the user.
            Ensure that relevant error handling message pop ups show.

        Logging In:

            On the login screen, enter a valid unique username and a password.
            Click "Login" to access the dashboard. Admin users will see additional controls for managing users.

        Logging InErrorHandling: 

            On the login screen, enter a valid Non unique username and a password or omit one of the values.
            Click "Login" to access the dashboard.
            Ensure that the appropriate error message shows on the bottom left. 


        CRUD Operations:

            Admin Role:
                Admin users can create, update, delete, and view all users. With the appropriate buttons enabled on screen initialisation.
                Test adding a user: Fill in the username, password, and role, then click "Add."
                Test updating a user: Select a user from the table, edit details, and click "Update."
                Test deleting a user: Select a user from the table and click "Delete."

            User Role:
                User roles have read-only access to the user list.
                Verify that buttons for adding, updating, and deleting are disabled.

        Error Scenarios:

            Attempt to register with a username that already exists.
            Try logging in with an incorrect password.
            Test navigation and button states for users with restricted access.
            Trying to Enter a new user in registration/UserView without all the fields filled and Updating without the values filled in the datagrid. 
            Trying to register, add or update a user with a name that already exists. 

        Future Improvements: 
            
            Add case sensativity for checking if a name already exists. 



