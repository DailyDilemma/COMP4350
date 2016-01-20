#Technology Selection and Justification

##Server Side Technologies
For our server side technology stack we considered the Java based Spring MVC Framework, LAMP servers with Django, and Microsoft’s ASP.NET. We selected ASP.NET due to comprehensive nature of the ASP.NET framework and the consistency and productivity of the Visual Studio development environment.

One of our concerns given the small amount of time to complete this project is the amount of time spent configuring our development environments to include all the components needed to meet the project requirements. Every technology stack we considered included some kind of unit testing framework, dependency management framework, etc. but the open source technology stacks relied on many small components that must be manually installed and configured to work together. A simple installation of Visual Studio on any Windows system provided nearly all the components required for us to complete this project in one installation and most of those components came preconfigured to work together. As far as performance and quality is concerned ASP.NET can easily produce quality web applications that perform just as well if not better than the open source frameworks.

ASP.NET works best with other Microsoft products, so our server side technologies are exclusively Microsoft and we have chosen to use SQL Server as our database, Entity Framework as our ORM, and IIS as our web server. 

##Client Side Technologies

###Web
For our front-end web framework, we considered Bootstrap and Foundation. We chose Foundation because the markup seemed cleaner and the built in library of components more closely matched what we want to do with our app.
For our web application frameworks we considered AngularJS, ReactJS and using nothing at all. We chose to try AngularJS because we wanted two-way data binding and liked the declarative programming model, however we may still drop it if it gets in the way more than it helps.

###Android
For Android we’ll use the default Java-based framework from Google. We were told we couldn’t use hybrid frameworks to generate both our web and mobile interfaces with a single framework and all other frameworks for Android focus on cross platform compatibility rather than single platform productivity. 
