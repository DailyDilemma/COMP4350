2016-02-26 21:54
Josh Westlake
How
How's it goin Matt? Or anyone currently banging their head against it

2016-02-26 21:55
Matt Deutscher
looking

2016-02-26 21:55
Mitch Lenton
I'm still working.

2016-02-26 21:57
Josh Westlake
k. Not trying to nag just seeing if anyone is stuck (as I was)

2016-02-26 21:57
Matt Deutscher
seems like the integration tests are the old ones with manual ids instead of the auto generated ones, and the api integration tests are trying to drop the db and start a new one while the old one is in use...

2016-02-26 22:00
Josh Westlake
Ya it's a bit of a mess. I figured the next thing to do would be to forget the WebAPI tests for the moment since they keep competing for the database and just try to get the tests that are in the current master branch to work. Then, time permitting, try to incorporate the WebAPI tests.

2016-02-26 22:01
Mitch Lenton
I have some ideas that I'm trying, so hopefully I can restore the project.

2016-02-26 22:01
Matt Deutscher
i'm going the slow route, manually trying to change them back, hope you idea works...

2016-02-26 22:10
Mitch Lenton
Alright, checkout webapi-fix-2. I think I retrieved most of it.
Some tests are failing, but that might just be on my end of things. Not sure yet.

2016-02-26 22:11
Josh Westlake
k, taking a look

2016-02-26 22:12
Mark Cortilet
Sometimes the tests failed for me too
Hen they'd pass the next time

2016-02-26 22:12
Mitch Lenton
I'll give it a run again.

2016-02-26 22:20
Mitch Lenton
Hmm...the failed tests are due to a SQL server connection issue.

2016-02-26 22:21
Matt Deutscher
yeah, says can't drop db because it's in use

2016-02-26 22:32
Mark Cortilet
it might be how asp.net runs tests
they get run in whatever order

2016-02-26 22:32
Josh Westlake
Every test class is trying to call DBInitializer independantly, and its creating a race condition depending on which tests it starts first. I'm trying to find a way to decouple it somehow.

2016-02-26 22:32
Mark Cortilet
right
make a wrapper class?

2016-02-26 22:34
Daniel J. Bouchard
How about a singleton?

2016-02-26 22:34
Mark Cortilet
was thinking more of a test suite

2016-02-26 22:34
Josh Westlake
Can't think of how to implement that off the top of my head. At the moment I'm considering passing some kind of parameter that lets you choose the name of the database. Then each class creates and drops it's own database each tests and they never collide.
Just spitballing though, that may be way hard than I think it is.

2016-02-26 22:35
Daniel J. Bouchard
Is this in master or web api fix 2?

2016-02-26 22:36
Josh Westlake
Definitely colliding in webapifix 2, master seemed okay to me earlier but I think Brent said he had problems with it not too long ago.

2016-02-26 22:37
Mark Cortilet
i have problems with the original one, just never ran into them since i was running tests as i made/fixed them

2016-02-26 22:38
Brent Rempel
I think the problems I had in master were just related to selenium and having the right start up

2016-02-26 22:41
Daniel J. Bouchard
Cool, running tests now to see what's up.

2016-02-26 22:41
Mark Cortilet
there's something called playlists for tests, trying it now

2016-02-26 22:42
Mitch Lenton
Anyone else have problems running the webapi integration tests?

2016-02-26 22:44
Daniel J. Bouchard
Erm, I'm not sure how to run those tests. I've gotten so far as to see the selenium testing on the main app.
Which all pass, btw.
Ah, I found a bunch of API tests in webapi-fix-2

2016-02-26 22:58
Daniel J. Bouchard
Mark, should there only ever be one instance of list assist context?

2016-02-26 22:58
Mark Cortilet
yeah, thats the issue
VS runs tests in parallel

2016-02-26 22:59
Daniel J. Bouchard
Kay, I'll see if I can't make it a singleton.

2016-02-26 22:59
Mark Cortilet
that won't help

2016-02-26 22:59
Daniel J. Bouchard
No?

2016-02-26 22:59
Mark Cortilet
it'll just make the tests fail in a new way

2016-02-26 23:00
Daniel J. Bouchard
Oh, you've tried that already. lol, k

2016-02-26 23:01
Mark Cortilet
no, but since there can only be one instance of the db, the tests will blow up when they try and create one when another one already has
right now, i think josh's suggestion is the only choice. there's no way to disable the parallel running

2016-02-26 23:02
Daniel J. Bouchard
Yeah, it's a good suggestion. I'll look into it.

2016-02-26 23:03
Mark Cortilet
already on it. its not so bad

2016-02-26 23:03
Daniel J. Bouchard
Okay. Let me know if you need any help.

2016-02-26 23:26
Mark Cortilet
problem: i'm creating a new context in the query files. how can i get it to point at the one in the test?

2016-02-26 23:28
Josh Westlake
Not sure. It's probably time to start thinking about fall back scenarios.
Maybe we just turf the tests that don't work?

2016-02-26 23:29
Mark Cortilet
can point out the current failures are due to parallel runs

2016-02-26 23:29
Josh Westlake
Sure, I guess we can keep em too and just cover the issue in documentation.

2016-02-26 23:32
Mark Cortilet
sec, may have got it

2016-02-26 23:46
Mark Cortilet
so close. down to 2 failing

2016-02-26 23:46
Daniel J. Bouchard
*photo or sticker*

2016-02-26 23:47
Josh Westlake
Well, that's as close as any of us have gotten. Might as well wrap that branch up. Does it still have all your other changes to the API?

2016-02-26 23:48
Mark Cortilet
it does. just let me make sure i didn't mess the controllers up
best hand in the other version. controllers had to get changed
unless you know how to create a controller that uses the context in global.asax

2016-02-26 23:59
Josh Westlake
Alright, I quickly cleaned up my branch and submitted it. It should have your latest changes but the tests are a mess. I'll try to clarify for the TA what happened, see if that helps at all.

2016-02-27 00:00
Mitch Lenton
ok, sounds good.

2016-02-27 00:01
Mark Cortilet
it might. parallel tests are a known issue for vs
and i'm within spitting distance of fixing it

2016-02-27 00:12
Brent Rempel
Do you think they are actually taking snapshots of our repos at midnight? Can we see if they have?

2016-02-27 00:14
Mark Cortilet
if they don't, we might be fine. just got api working
and main works too

2016-02-27 00:15
Josh Westlake
They would probably just branch off master and refer to their branch if they had
Feel free to try and add a fix

2016-02-27 00:17
Matt Deutscher
I'm sure there is a little leeway

2016-02-27 00:18
Josh Westlake
As for me I'm done for the day, I'm still running off 2 hours sleep from yesterday. There were some minor issues with the deployment, but I'll fix them in the morning and they won't notice either way. If anyone gets an improved branch posted I'll deploy it tomorrow if it has any changes.

2016-02-27 00:29
Daniel J. Bouchard
Sounds good.

2016-02-27 00:46
Mark Cortilet
k, better branch checked in as frequency-start. includes all my previous changes and the ones i just finished

2016-03-03 12:48
Matt Deutscher
Android repo is up with a basic template, link on the project github page

2016-03-10 10:46
Josh Westlake
Was the day I missed the day he handed out Android devices in class? I'm trying to figure out what API level we're building things for, was hoping one of you had the device and could tell me what version of Android it's running.

2016-03-10 10:48
Mitch Lenton
Yeah you weren't there for that. Daniel has it but I'm not sure of the Android version.

2016-03-10 10:51
Josh Westlake
K, thanks Mitch. Next time you're playing with the device Daniel see if you can find the android version so we can optimize the project APIs to target it.

2016-03-10 10:53
Daniel J. Bouchard
Okay, sounds good. I'll have a look once I get home after school.

2016-03-10 14:45
Daniel J. Bouchard
Hey guys, the tablet is on 6.0.1

2016-03-10 14:46
Josh Westlake
Perfect, thanks.

2016-03-10 15:12
Daniel J. Bouchard
Hey Matt, could you add us as contributors to the Android project you set up please?

2016-03-10 15:13
Josh Westlake
Hold on a sec on that.
If you are still having trouble like I was Daniel I'm just about to publish a new repository. I'd be curious to see if it works for you out of the box.

2016-03-10 15:14
Daniel J. Bouchard
Kay.

2016-03-10 15:16
Josh Westlake
I made you a collaborator on https://github.com/DailyDilemma/ListAssist, do you have time now to pull it down and see if it works for you?

2016-03-10 15:17
Josh Westlake
... arg my damn face

2016-03-10 15:17
Daniel J. Bouchard
I do.
Is that the normal project?

2016-03-10 15:18
Josh Westlake
nah the normal project is called COMP4350

2016-03-10 15:18
Daniel J. Bouchard
Ah, kay.
Would I import or open it?

2016-03-10 15:22
Josh Westlake
I forgot what the wizard says, do you see an options for New Project -> From Version Control?

2016-03-10 15:23
Daniel J. Bouchard
I did see and use that one. Now I'm getting an invalid gradle configuration error.

2016-03-10 15:24
Josh Westlake
Hmmm. Does it allow any kind of drilldown? I wonder if it's just something with my local path in it somewhere.

2016-03-10 15:24
Daniel J. Bouchard
That's my bet. I'm looking it up now

2016-03-10 15:27
Josh Westlake
I just noticed gradle.xml on my machine is located in the P: drive, check gradle.xml and maybe change that to C for you

2016-03-10 15:28
Josh Westlake
*photo or sticker*

2016-03-10 15:36
Daniel J. Bouchard
It seems to be working now. I just got the emulator and things running, I just had to try to import gradle again.
Now that it's running I'm going to take a break and go do some OS homework.

2016-03-10 15:40
Josh Westlake
Okay I'll add everyone as a contributor to my repository. It sounds like whether you use my template or Matt's template you'll run into problems, but if people have less issues with mine we'll go from there.

2016-03-10 15:58
Matt Deutscher
So, yours then? If so I'll delete mine and pull yours

2016-03-10 16:33
Josh Westlake
Don't delete yours just yet. All we know is mine worked better for Daniel, since I made it locally the fact that I could use mine doesn't prove much. If you do pull mine down and it works for you fairly easily that would be a good sign.

2016-03-10 16:48
Josh Westlake
Funny side story. I'm here in UC next to a group of high strung 3rd year comp sci students working on a group project together. The funny part is they don't speak much english, so the only words I can continually make out are "Error" and "VW date" as they scramble to figure out their project.

2016-03-10 16:49
Daniel J. Bouchard
*photo or sticker*

2016-03-10 17:01
Matt Deutscher
That's pretty awesome.

2016-03-11 18:04
Josh Westlake
Marks for iteration 2 were posted. 19.5/25 or ~78%. We're still in this. :)

2016-03-12 13:10
Josh Westlake
6 days left! I know more than half of us are struggling with our OS assignment but I thought I'd throw a few things out there in case anyone needs a break from other homework. So...
- I made a list in Trello called Iteration 3 and I'm putting everything we need to do in there
- The first card in the list is important and fairly easy https://trello.com/c/gl6XvmuH so anyone look for somewhere to start should look there.
- There hasn't been any more discussion on which Android repository we're going to use, so I suggest using mine https://github.com/DailyDilemma/ListAssist for no other reason than it resolves the discussion of which one to use
- When using my repository refer to the earlier conversation between Daniel and I on the initial issues with gradle

2016-03-14 18:45
Mitch Lenton
Hey all. I took a look at what we have to do (which is quite a lot lol). It might be a good idea to have a meeting tomorrow after class to allocate tasks and discuss what needs to be done by Friday.

2016-03-14 18:49
Josh Westlake
I have an appointment with a pediatrician right after class so I can't make it but it wouldn't hurt to meet without me. Also, if anyone is looking for something to do in the meantime you can try pretty much anything you can think of with the Android app or anything from the Iteration 3 list on our Trello board.

2016-03-14 18:50
Daniel J. Bouchard
A meeting would be good. I've done a good portion of the UI so far.

2016-03-14 18:52
Mitch Lenton
I think the main thing is just fixing or finishing stuff from the feedback such as having the UI pull the data from the Web API. After that we can implement the rest of the user stories that we are missing.

2016-03-16 12:03
Josh Westlake
As far as rest clients go, I've taken a look at Retrofit from Square and RestTemplate from the Spring libraries. Retrofit seems awesome but RestTemplate is way simpler. I almost have RestTemplate working, hopefully by a little later today if I stop getting interrupted.

2016-03-16 12:47
Mitch Lenton
I'll check out some in a bit. I'm close to getting the web interface to consume the web api rather than access the database directly. I'll post a branch once I have finished.

2016-03-16 18:17
Matt Deutscher
I was setting up some tests, in the md-tests branch. FIgured we needed some layers in the app as well, and a stub list of lists to test off of. Still need to do more, it's not finished yet, but take a look if you have some time

2016-03-16 21:00
Daniel J. Bouchard
Have we made any more headway on the API? I'd love to start getting my hands dirty working on it.

2016-03-16 21:10
Mitch Lenton
I'm still working on consuming the API from the web interface. I can pull data with no issue, but it won't post anything.

2016-03-16 21:15
Daniel J. Bouchard
Okay, so should I just try myself and see what I can do.

2016-03-16 21:17
Mitch Lenton
I'll put up a branch with my current progress. I'll keep working on it, but if someone else finds a solution, awesome.

2016-03-16 21:22
Daniel J. Bouchard
Let me know when the branch is up.

2016-03-16 21:23
Mitch Lenton
Will do. It will just be a minute.
Ok, it's posted and is called mitch-consume-api. There are a couple of packages that I added which you can see in the commit.

2016-03-16 21:28
Daniel J. Bouchard
Kool. I'll have a look in a bit.

2016-03-16 21:28
Mitch Lenton
Ok sounds good.

2016-03-16 21:40
Mark Cortilet
is it just the add new list thats causing problems? cause i've fixed that

2016-03-16 21:42
Mitch Lenton
I don't think so. Every time a send a post or put request to the api, it sends back a response of "Method Not Allowed".
*I

2016-03-16 21:49
Daniel J. Bouchard
I don't see any branches but master in DailyDilemma/ListAssist

2016-03-16 21:50
Mitch Lenton
This is for the main repo api. I haven't done anything with the android api functions.

2016-03-16 21:50
Daniel J. Bouchard
Ah, kay. Sounds good.

2016-03-16 22:08
Josh Westlake
I'm just about to start looking at the Android rest client again. I spent the majority of today tearing apart my bathroom to fix what was probably the most clogged toilet in the history of the universe.

2016-03-16 22:09
Daniel J. Bouchard
lol, nice. I'm also looking into it right now.

2016-03-16 22:53
Daniel J. Bouchard
So, I just found out that the API returns XML...

2016-03-16 22:54
Josh Westlake
It returns whatever you ask it for in the headers of your request. XML is default, JSON is available by request. It just depends what method you use to request it.
That's standard for API's

2016-03-16 22:54
Daniel J. Bouchard
Okey Dokey, I was just learning that.

2016-03-16 22:58
Josh Westlake
Alright I have something working against our currently deployed Amazon API. It just does GET requests right now but you get the idea. I just need to time annotate it so it makes sense for everyone what is going on, apparently Android requires network requests be handled on a separate thread so there is some asynchronous nonsense that has to happen.

2016-03-16 23:25
Josh Westlake
Alright, it's merged. It's not connected to the UI or anything but it does call one List from the API and put the results into a Java object. We can build from there.

2016-03-16 23:46
Daniel J. Bouchard
I'm still slightly confused. Where do you call the API?

2016-03-16 23:47
Josh Westlake
in the doInBackground method of the HttpRequestTask class inside the main activity

2016-03-16 23:48
Daniel J. Bouchard
Ah, cool.

2016-03-16 23:48
Josh Westlake
The result of the call returns to the onPostExecute task which is were you'd make your calls back to the UI
err by task I meant method

2016-03-16 23:49
Daniel J. Bouchard
Sounds good

2016-03-17 00:09
Mitch Lenton
Just a quick update. I've got the post requests dealing with the list operations now working. I just need to do the post requests for the list item operations and the web interface will be communicating directly with the api.

2016-03-17 00:10
Josh Westlake
Good to hear. I wasn't sure if we'd actually be able to get those marks, hopefully all goes well :)

2016-03-17 00:11
Mitch Lenton
Hopefully!

2016-03-17 17:06
Josh Westlake
I updated the Android app to populate the initial list of lists from the WebAPI. I'll have to modify the WebAPI to return IDs when getting lists so we can perform update/delete operations easily, but first I have to hold a baby for an hour.

2016-03-17 18:53
Mitch Lenton
I'm almost done with the interface to api fix. I had to make a few alterations to the web api to allow for certain operations such as retrieving a specific list item. Once I'm done, I'll post a branch and let you all know.

2016-03-17 19:02
Josh Westlake
Oh... I wonder if I just made and deployed the same change. I just made the ID field public so the api returned it as part of the list information.

2016-03-17 21:23
Mitch Lenton
OK. I have finally got the interface to work against the API. There are some tests that will need to be adjusted, but as far as I can tell there are no issues. I've published a branch on the main repo called mitch-interface-to-api. Let me know what you guys think.
Also, I had to make some adjustments to the web api to work with the interface.

2016-03-17 21:26
Daniel J. Bouchard
Did you change making lists?

2016-03-17 21:42
Mitch Lenton
There were some changes to some of the controller parameters and parameters in ListQueries. Nothing major. I also added some functions to ListQueries and to the Web API controllers.

2016-03-17 22:20
Daniel J. Bouchard
I'm getting a response of 500 from my put method when making lists. Is this a known issue, or is that on my end?

2016-03-17 22:20
Mitch Lenton
Hang on, I'll check.

2016-03-17 22:20
Daniel J. Bouchard
Kay

2016-03-17 22:23
Mitch Lenton
For creating new lists, it's a post request.

2016-03-17 22:34
Daniel J. Bouchard
Yeah, I'm going one of those.
Hey Josh, how do I get the name of a shopping list item.
*doing

2016-03-17 22:36
Mitch Lenton
hmmm...this is from the android app?

2016-03-17 22:36
Daniel J. Bouchard
Yup

2016-03-17 22:37
Mitch Lenton
Do you have the code on the repository?

2016-03-17 22:39
Daniel J. Bouchard
By that do you mean do I have a copy of COMP4350 repo on my machine?

2016-03-17 22:41
Mitch Lenton
No, the code that you are working with for android.
Oh wait. Is this running off the aws api?

2016-03-17 22:46
Daniel J. Bouchard
Yeah.

2016-03-17 22:48
Mitch Lenton
My revisions to the web api haven't been pushed to aws. So it is using the existing api functions. I haven't merged my changes with the master branch yet as I wanted to make sure that there were no conflicts that anyone could see.
I'll make a pull request so everyone can see it easily.

2016-03-17 22:55
Daniel J. Bouchard
Sound good. Do the changes pad the tests?
*pass
If so, you should probably just merge it.

2016-03-17 22:56
Mitch Lenton
Some, there are still some that fail. I'll get them fixed.

2016-03-17 22:57
Daniel J. Bouchard
Sounds good. I'll continue finishing the skeleton code.

2016-03-17 23:00
Josh Westlake
@Daniel re: your shopping list item comment the API doesn't have a GET for list items, so you just get an entire list and it will have the name of each item as a collection in the list object

2016-03-17 23:03
Mitch Lenton
Should I merge just so you can use the changes while I work on the tests?

2016-03-17 23:05
Josh Westlake
Go ahead and merge, I looked it over briefly but it's a lot of code so if you think it's good that should be fine. I'll deploy everything shortly after you merge so it can be tested easily from Android devices and we can see if it functions properly in the cloud as well.

2016-03-17 23:05
Mitch Lenton
Done.

2016-03-17 23:13
Daniel J. Bouchard
*photo or sticker*

2016-03-17 23:21
Daniel J. Bouchard
Btw, Josh, I started working on the single list view.

2016-03-17 23:22
Josh Westlake
Okay cool.
Hey Mitch do you remember off the top of your head which file has the connection information for talking to the WebAPI? I think we might need different info for release builds

2016-03-17 23:26
Mitch Lenton
LAListsController.cs is the only place where I specify the connection info. It's on line 20.

2016-03-17 23:27
Josh Westlake
k, thanks

2016-03-17 23:27
Mitch Lenton
no prob.

2016-03-17 23:40
Josh Westlake
k a deployed version is up for both. A quick tour around the app and the only thing I notice that is off is that adding a list item doesn't seem to work. I haven't checked the local version yet, testing that shortly
http://ec2-52-36-187-54.us-west-2.compute.amazonaws.com/LALists in case anyone wants to test production

2016-03-17 23:43
Mitch Lenton
Strange. It works fine on the local.

2016-03-17 23:43
Josh Westlake
k, maybe there is something odd about the API deploy. I'll keep checking.

2016-03-17 23:43
Daniel J. Bouchard
I haven't made adding list items yet.

2016-03-17 23:44
Josh Westlake
I'm talking about the web app front end, is that what you mean Daniel?

2016-03-17 23:44
Daniel J. Bouchard
Derr, no. My head is still in the Android app.

2016-03-17 23:53
Mitch Lenton
It's strange. I inspected the request and response headers and the response header is sending back a 200 status code but it didn't actually add anything. Same thing with the remove. It would only redirect back to the same page if the status code was not 200.

2016-03-17 23:54
Daniel J. Bouchard
Hmmm, the web app doesn't seem to show if an item is already checked. That or it doesn't successfully check the item when you click save.
I think the response body might have an error for you to inspect?

2016-03-17 23:56
Mitch Lenton
The body just contains the html for the view. No errors at all. Weird.

2016-03-17 23:56
Daniel J. Bouchard
Taught is.
*that

2016-03-18 01:35
Josh Westlake
There are a few problems on the go. Locally the web app front end doesn't save checked boxes, that seems to be an issue regardless of which version. The deployed version of the API is having more problems, and responds differently when attempting to PUT/POST saying that the type of request is not allowed for the given verb. Apparently swagger has different rules for local and remote address and some other nonsense about schemas I can't quite figure out yet. I'll continue to work to try and get the remote version identical to the local, but it'll have to be tomorrow. I need some sleep.

2016-03-18 01:36
Daniel J. Bouchard
Yeah, same here. I committed all the work I'll be committing before tomorrow morning.

2016-03-18 01:37
Mitch Lenton
OK. We have till 4 tomorrow, so lots of time left. I need to work on my OS 2 assignment now, so I will continue with this tomorrow.

2016-03-18 01:38
Daniel J. Bouchard
Yeah, I'll start home tomorrow and work on it as well.

2016-03-18 10:46
Josh Westlake
Things I know are left to do...
- I am still working on determining why the front end to WebAPI connection doesn't work when it's deployed. I know roughly what's happening, but debugging is going to take a while.
- The WebAPI needs some work, several things are out of place (on the UI page at least) and I'm not sure what is left to do under the hood. On the UI page, POST and PUT for ListItems is backwards, POST adds new items and PUT updates them but we have the opposite. Several of the headers (ie. /api/ListItems) are missing the {id} indicator showing they take parameters and the PUT function for lists doesn't allow anything to be entered.
- If we do require changes to the API it will effect our clients so we need the API sorted out stat.

2016-03-18 11:00
Mitch Lenton
I'll be working on it all after noon (and probably during my class lol).

2016-03-18 12:03
Daniel J. Bouchard
I've started poking around in the API now.

2016-03-18 12:34
Josh Westlake
Finally got the production web api working, so the front end should work in production the same way it does in dev now.

2016-03-18 12:35
Daniel J. Bouchard
*photo or sticker*

2016-03-18 12:38
Daniel J. Bouchard
Is it published on Amazon?

2016-03-18 12:41
Josh Westlake
What is on amazon right now is a fixed version of yesterday's build. The server itself was blocking PUT/DELETE methods internally, so those should be working now but if there have been API updates since yesterday those aren't up yet.
After I evaluate a pull request I'll merge everything and send the newest up.

2016-03-18 12:51
Mitch Lenton
Good to know that I didn't miss something lol.

2016-03-18 13:09
Josh Westlake
Daniel how goes the Android stuff, are you waiting on anything at the moment?

2016-03-18 13:10
Daniel J. Bouchard
Nope. I'm just finishing up getting the IDs attached to the UI. Once that's done it's all testing from there.

2016-03-18 13:11
Matt Deutscher
im working on android tests now

2016-03-18 13:12
Josh Westlake
Okay, and Mitch were you doing anything with the API? You mentioned you might work on it this afternoon so I just want to check if you have any changes in the pipe that might affect the Android or web clients.

2016-03-18 13:14
Mitch Lenton
Mainly just cleaning up the things that you mentioned.

2016-03-18 13:37
Daniel J. Bouchard
Hey Josh, do you think you could look at the LAList(formerly ShoppingList) file. It doesn't seem to be getting any ID and I don't want to immediately blame the API.

2016-03-18 13:40
Josh Westlake
Sure, that file was autogenerated using an online utility that automatically builds your POJO when you supply it with the JSON it represents. If it hasn't been regenerated that'd be the problem.

2016-03-18 13:41
Daniel J. Bouchard
Erm, I went in and added the ID manually. Perhaps that broke it.

2016-03-18 13:42
Josh Westlake
http://www.jsonschema2pojo.org/ is the site I used, it adds all the Jackson2 annotations for you, so if you copy the JSON shown in the WebAPI into that box and generate it you should be able to see what you're missing

2016-03-18 13:42
Daniel J. Bouchard
Also, on another note, we should probably have an API call that unchecks the item.
Kay. I'll go look at that.

2016-03-18 13:48
Josh Westlake
I think our API should handle checking/unchecking within the existing PUT commands for Lists and ListItems. That's more in line with RESTful services. You PUT an item to replace the one that is already there, you don't typically toggle individual attributes. From the Android app when we do PUT ListItem we should pass the whole ListItem and the API should update it. From the front end, when we click save, the entire List (including all children ListItems) should be saved and if any were checked or unchecked the database would just update.

2016-03-18 13:49
Josh Westlake
Granted, with 131 minutes left... do what you need to do to make anything work :P

2016-03-18 13:52
Daniel J. Bouchard
Sounds good.

2016-03-18 13:53
Matt Deutscher
when I try to run it on the emulator, I get a ListAPIHelper: Failure to get id error, and no lists populate

2016-03-18 13:56
Daniel J. Bouchard
Yeah, that was my trouble with the IDs that I asked Josh to help return. I should be close to having it fixed.

2016-03-18 14:06
Matt Deutscher
now when you click any of the open buttons, it says failure to find id for opening list ( or deleting list if you click that one)

2016-03-18 14:06
Daniel J. Bouchard
Yup, trying to figure out where to put the ID so that I can grab it later to open.

2016-03-18 14:07
Matt Deutscher
k

2016-03-18 14:17
Daniel J. Bouchard
List IDs are sorted out, I'm still having trouble with item IDs.

2016-03-18 14:19
Josh Westlake
How so?

2016-03-18 14:22
Daniel J. Bouchard
The ID isn't coming in the API call

2016-03-18 14:29
Josh Westlake
Same issue as before, just need to regenerate the ListItem POJO?

2016-03-18 14:29
Daniel J. Bouchard
I'm trying that again now.

2016-03-18 14:32
Josh Westlake
Mitch does any of your work with the API change the PUT/POST methods or return any new fields? I'm curious if there is anything I can do about the fact that our checkboxes don't save, but I'll have to funnel any changes through the API.

2016-03-18 14:33
Daniel J. Bouchard
Doesn't look like the problem is on my end, wanna check the API?
I'm getting the description and checked value, just not the id

2016-03-18 14:36
Josh Westlake
Specially what request are you making from the API, are you doing a GET ListItem? The response shows they are there.

2016-03-18 14:36
Josh Westlake
*photo or sticker*

2016-03-18 14:37
Mitch Lenton
The only major change I made was reversing the POST and PUT for the functions that you mentioned earlier. Right now I am configuring the route information to display more info in the swagger ui which shouldn't affect any api calls.

2016-03-18 14:39
Josh Westlake
K, you think that might be done by 3? We'd need time to make sure the front ends are still calling things correctly after the change.

2016-03-18 14:40
Mitch Lenton
Should be.

2016-03-18 14:41
Josh Westlake
*photo or sticker*

2016-03-18 14:41
Josh Westlake
Daniel I also get all the ID's for listitems returned when I ask for an entire list. It doesn't sound like the API unless I am missing something.

2016-03-18 14:41
Daniel J. Bouchard
It's working now. I forgot a ! In my logic.

2016-03-18 14:42
Josh Westlake
k, awesome

2016-03-18 14:54
Daniel J. Bouchard
Not sure how, but in ITo do list there's an item with a null description...

2016-03-18 14:58
Josh Westlake
We don't have any validation on the field, the TA was testing and put that in there.

2016-03-18 14:58
Daniel J. Bouchard
K

2016-03-18 14:59
Mitch Lenton
Alright, the api changes are up in the branch mitch-api-updates. Should I merge?

2016-03-18 15:00
Josh Westlake
I'll pull it down and take a quick look first.

2016-03-18 15:03
Mitch Lenton
ok. Quick note is that you may need to modify the urls for the android api calls to match the new paths for the following functions: Create list, Delete list, Remove list item, and add list item.

2016-03-18 15:03
Daniel J. Bouchard
Okay

2016-03-18 15:03
Mitch Lenton
Not huge changes, but it displays better in swagger.

2016-03-18 15:03
Daniel J. Bouchard
Kool

2016-03-18 15:04
Josh Westlake
Does the web front end work with the modified calls?

2016-03-18 15:05
Mitch Lenton
For example, before create lists had the url api/Lists?listName=something. Now it's just api/Lists/<listname>.

The web front works as far as I can tell.
*web front end

2016-03-18 15:12
Josh Westlake
The API changes blew up for me. Won't even start, could be my machine. Not sure. If I don't figure it out in 5 minutes that'll be where development stops.

2016-03-18 15:13
Mitch Lenton
Odd...

2016-03-18 15:13
Daniel J. Bouchard
Lovely.

2016-03-18 15:14
Mitch Lenton
Make sure to clean then build.

2016-03-18 15:20
Josh Westlake
Daniel how stable is what you have right now?

I got it working, but it'll be another 10 minutes to deploy after which Daniel has to fix all is existing stuff to work after which Matt has to follow up with tests. I don't think that's gonna happen.

2016-03-18 15:20
Daniel J. Bouchard
Well, I'm pretty close to done. I'm getting all the item API calls up and working without crashes.

2016-03-18 15:22
Josh Westlake
Okay, mitches changes go in next iteration. Everyone clean up and make what you have as stable as possible.

2016-03-18 15:23
Mitch Lenton
ok sounds good. I'll work on getting tests fixed.

