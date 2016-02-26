#Iteration 2 - Summary of Changes

##What Made It In

* Finished the remaining items from Iteration 1, specifically...
  * Adding a new item to a list [[Issue #17](https://github.com/DailyDilemma/COMP4350/issues/17)]
  * Removing an item from a list [[Issue #20](https://github.com/DailyDilemma/COMP4350/issues/20)]
  * Checking an item off the list [[Issue #19](https://github.com/DailyDilemma/COMP4350/issues/19)]
* Automated Acceptance Testing with Selenium
  * NOTE: Requires Firefox! With Firefox installed run all tests from Visual Studio and the acceptance tests will automatically launch the browser and report their success in the Visual Studio test runner window.
* The entire project is deployed to an AWS EC2 instance. To access the front end or API click the following links...
  * [Front End](http://ec2-52-36-187-54.us-west-2.compute.amazonaws.com/)
  * [Web API]()
* Following up on the feedback from Iteration 1, we...
  * Removed the ID field from the list creation screen, the system now properly assigns an ID automatically without prompting.
  * A queries class was created to establish an interface between the controllers and the database so the controllers don't call the database directly.
 
##What Didn't Make It In

##The New Plan
