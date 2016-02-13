#Installation Guide

##Requirements

- Visual Studio 2015
    - Enterprise Edition may be required to view UML Diagram
    - SQL Server Data Tools 
      - This is installed by default with Visual Studio. If you don't see 
        a "MSSQLLocalDB" database installed locally then you'll need to add this 
        to your installation so the project has access to a local database.

##Usage
Right now everything can be run from within the Visual Studio environment. Next
iteration it will be deployed to AWS and full deployment instructions should be
available then.

To start, load the ListAssist solution in Visual Studio 2015. From there follow the
instructions for the module you want to use.

###ListAssist (Web Application)
- With the ListAssist project set as the startup project (should be by default),
  press F5 or click the play icon to start debugging.
- On first run the project should create it's own local database in MSSQLLocalDB
  and seed it with initial values. If you want to use a different database
  change the connection string in the Web.Config file of the ListAssist
  project.

###ListAssistAPI (WebAPI)
- Right click the ListAssist.WebAPI project and select "Set as Startup Project"
- Press F5 or click the play icon to start debugging.
- After the initial pages loads click on "SwaggerUI" in the top navigation bar.
- The SwaggerUI page will list the available API functions and give options for
  testing API calls
- NOTE: Do this *after* you have run the primary web application as running the 
  web app builds the database the API needs to run

###ListAssist Testing
- Standard Visual Studio test suite, press CTRL-R,A to run all tests.
- If the test explorer window doesn't open automatically, from the main menu go
  to Test -> Windows -> Test Explorer

