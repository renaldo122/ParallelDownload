# ParallelDownload

Framework and Tools

.NET Framework 4.7.2: https://dotnet.microsoft.com/download/dotnet-framework/net472
MSTest.TestFramework: https://www.nuget.org/packages/MSTest.TestFramework/
AutoMapper: https://www.nuget.org/packages/AutoMapper/
Autofac: https://www.nuget.org/packages/Autofac/

Solution: AisCodeChallenge
Details regarding directories in the attached solution:
•	AisCodeChallenge.Common
•	Cache ->Used to store data in memory for better performance. 
•	Common -> Implements common functions across application
•	Exceptions -> Used this directory for Custom exceptions, to represent errors that occur during application execution
•	Extensions -> Method extensions for formatting exception messages
•	Message -> Used to pass parameters from service to JavaScript.
•	Model -> Specific data and application logic
•	AisCodeChallenge.Services
•	File –> Interface where are implemented all methods for getting and downloading files
•	AisCodeChallenge.Web.MVC
•	Controllers -> Implements action result for handling request.
•	Extensions -> Implements different extensions 
•	Factories -> Converts one model to another
•	Infrastructure -> Implements AutoMapper, registers dependency registrations with Autofac, loads data in application startup.
•	Scripts -> App folder under Scripts contains all the scripts files I have created for this solution, contains functions, constants and helper
•	ViewModels -> Created view model to use in view and have a separate logic from model
•	Views -> At File folder I have created views for grid, toolbar and lists.
•	AisCodeChallenge.Tests
•	Base -> Implements a Base class methods for initializing Interface
•	Configuration -> Configuration used in testing
•	Extensions -> Assert Extensions for objects
•	Helper -> Configures and Initializes classes
•	Services -> Contains all tests for methods developed in Application


The task:
1- Lists the currently available 10 files -> I used the class library (AisUriProvider) to get list of files.  
2- Refresh the list automatically every 5 minutes, or when the user’s requesting it. We can call this the sync operation. -> Created partial view for lists of files and sent request from JavaScript to refresh only one certain part.
3- Store the last 10 files locally and load it on app start-up. -> The way I thought about this was: First time the application starts this list is empty. Every time I get the list or user refreshes the list, I store the last 10 URLs in a text file. On Application start-up I read the text and download all the files locally and read directory of files to display the list. (In layout is the list on the right section of the page).
4- Delete any unnecessary files after syncing. -> On Application start-up I delete all the directory with previous files, and then populate it with the proper ones.
5- Download the files in a parallel way, where the user should be able to specify the degree of parallelism. For example, you shouldn’t run more than N tasks at a time and there should not be download tasks explicitly waiting for each other. -> For downloading files, I have created an Async method with parallel task when I get all the list of URLs that user has in grid.
6- The user should be able to cancel sync operations-> Created a method for cancellation when I dispose the CancellationTokenSource used for Downloading files.
7- The application should be able to represent the files visually, preferably in grid -> Created a partial bootstrap grid for displaying data 
8- If the file is an image, the application should display the image -> Display image in grid if file has a certain extension (.jpg, .png, .gif, .bmp, .jpeg, .ico, .tiff, .bmp) 
9- If the file is text based, you should show the preview of the text-> Display the content of file with certain extensions (.dotm .dotm .htm, .rtf, .mht, .mhtml, .html, .odt, .txt, .css, .js) 
10- For anything else you should show a placeholder ->Displayed a text (Invalid format to preview)
11- Errors should be handled gracefully, and displayed to the user -> Created an extension to format exceptions to get message.
12- Display a dialog to the user if they want to exit while files are being downloaded. The user should be able to quit anyway. -> Created a state in JavaScript to trace if the files are downloading and on onbeforeunload I display a dialog box if user closes the browser.
13 - Test We would like to ask you to pay attention to code quality. Write a clean code, which is easy to test (and test it) and the concerns are well separated from each other. We’re interested in how you would architect a solution from the ground up. For any details not covered by the requirements or that are vague it is up to you to deal with it. Please leave your comments, assumptions and decisions in a separate text file. -> Created a Unit testing project and tested all the methods.
