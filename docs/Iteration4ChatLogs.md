2016-03-18 15:45
Daniel J. Bouchard
I think I'm done guys. If anyone has time, check it out and let me know.
Everything works on my phone, but when I use the tablet I get an error: unable to resolve ec2 URL: no address associated with hostname.
Derr, the tablet isn't connected to the Internet.

2016-03-18 15:51
Matt Deutscher
just going to update the chat logs

2016-03-18 15:52
Josh Westlake
I did most of it, there is only the past 15 minutes or so missing

2016-03-18 15:52
Matt Deutscher
nevermind...

2016-03-18 16:00
Mitch Lenton
Android app works great! Runs just fine on my Nexus 7.

2016-03-18 16:01
Daniel J. Bouchard
Yup, I connected it to the Internet and mine worked too :-P

2016-03-18 16:08
Daniel J. Bouchard
Kay, I'm leaving now. Good luck everyone!

2016-03-18 16:08
Mitch Lenton
Alright, cya!

2016-03-30 14:19
Josh Westlake
I just spent over an hour trying to figure out how to get the Android project to build so I thought I'd share what I learned and maybe save others some time. 

If you clone the repository and find that Android Studio shows an empty project, the reason is that we don't store the project (*.iml) files because the IDE generates them automatically when you use Gradle.

So, to make the project show up for you, open the Gradle pane on the right side of the screen and click the little "Refresh" button. Gradle will rebuild the project modules and create new *.iml files after which the project structure will appear.

2016-03-30 14:19
Josh Westlake
Hopefully the marker knows that.

2016-03-30 14:25
Matt Deutscher
Hope so.

2016-03-31 16:43
Josh Westlake
Our project presentation is (very likely) just under 5 days away. We should start to get organized.

Here are some shared documents we can start getting prepared:

Presentation Slides (PowerPoint Online)
https://onedrive.live.com/redir?resid=719DFDA28FD3D19F!98852&authkey=!ADrYz5v5LYcdhKg&ithint=file%2cpptx

Presentation Outline (Word Online) - a scratchpad of sorts to organize our thoughts
https://onedrive.live.com/redir?resid=719DFDA28FD3D19F!98854&authkey=!AIhT_dRItnEAOW8&ithint=file%2cdocx

2016-03-31 16:44
Josh Westlake
The retrospective will be easy, the actual demo we'll want to be more organized for and probably have a rehearsed happy-path scenario.

2016-03-31 18:43
Mitch Lenton
Thanks Josh. I'll be working on fixing some unit tests tomorrow but I'll give the powerpoint a read in a bit.

2016-03-31 18:44
Josh Westlake
The PowerPoint is more or less empty, I just threw it up so people had something to throw a slide or two in when they got some time.

2016-03-31 18:45
Mitch Lenton
Ok. I'll think of some things to put in.

2016-04-02 17:38
Mitch Lenton
I put up a new branch yesterday containing fixes to the integration tests. They all work (for me at least) and I had to make a few small changes to the ListQueries class.

2016-04-02 17:54
Josh Westlake
I saw your branch but I wasn't sure if you were done with it. If you are done with it and think it's ready to go make a pull request and I'll know it's ready for merging.
I've been working on adding our new feature. I have it about 80% done in the web application but I'm stuck on some of the WebAPI stuff so hopefully I'll have it finished tomorrow some time.

2016-04-02 17:56
Mitch Lenton
I'll give it a run again just to make sure then I'll make a pull request.

2016-04-02 18:24
Mitch Lenton
OK. Pull request created. The WebAPI tests still seem to fail though.

2016-04-02 18:28
Josh Westlake
K. I'm hoping I have a solution to some of our parallel problems in the branch I'm working on. Our ListQueries implementation uses a public *static* class which apparently is not good practice because it creates only a single instance of the ListAssistContext that all threads end up fighting for. A repository tutorial I found showed that the way to do it is to declare a new repository (ListQueries) instance in each controller so there is a different context for each thread.

2016-04-02 18:29
Mitch Lenton
That seems like what I have been seeing. The data layer integration tests run, but the WebAPI integration tests can't modify the database because it's currently in use.

2016-04-03 15:07
Josh Westlake
If someone gets some time I've been stuck for a few hours on the feature I'm working on and I think what it might need is a pair of fresh eyes.

Essentially I have the bulk of things working except that the "Suggestions" which have been seeded in the database (you can see them there when you open the LASuggestions table) aren't getting populated in the JSON returned by the WebAPI. There is a suggestions array in the object, but for some reason it's always empty even when the data is clearly in the database.

If someone with any familiarity with the web API can take a look that'd be great. I'll still be trying to fix the problem on and off but judging by how long I've been stuck it might be something simple I keep glossing over.

jw-AddSuggestionGUI is the branch. A visual explanation will follow...

2016-04-03 15:08
Josh Westlake
*photo or sticker*

2016-04-03 17:19
Mitch Lenton
I'm just about to head off to the gym so I can check it out in a bit. However it does look like a situation I had when redoing the controller last iteration. When I recieved list objects back from the web api, the list items were missing. It could be an issue with binding the data from the api to an object.

2016-04-03 21:28
Mitch Lenton
Figured it out. There was no mapping profile for the ShoppingListSuggestions in the ShoppingList mapping in the WebAPI. I've pushed the change to the jw-AddSuggestionGUI branch.

2016-04-04 00:20
Josh Westlake
You're the man Mitch I'll check it out in a little bit

2016-04-04 10:25
Josh Westlake
Daniel/Mark if you're in OS today and I don't get to you before you leave lets have a quick chat about the presentation for this after class.

2016-04-04 10:26
Daniel J. Bouchard
Sounds good

2016-04-04 10:29
Matt Deutscher
What time?

2016-04-04 10:30
Daniel J. Bouchard
After class works well for me. Is there another time you'd rather do it?

2016-04-04 10:30
Josh Westlake
Oh right, I forgot you'd be here too, it would be at around 2:20 outside E2-110

2016-04-04 10:30
Matt Deutscher
Ok. My class is in 350, I'll come straight down.

2016-04-04 16:44
Josh Westlake
We got 21/25 on our last iteration. Can't explain it, but I'll take it. https://github.com/DailyDilemma/COMP4350/blob/master/docs/Grades/Group_H_Iteration3_Marking.pdf

2016-04-04 16:52
Brent Rempel
Wow that's great news

2016-04-04 17:35
Daniel J. Bouchard
Hey guys, we can't use the tablet in class, so we'll have to do an emulator thing if we are going to demo the Android app at all.

2016-04-04 17:38
Brent Rempel
Do we know if there's expectation of us to demo the android app? I guess he hasn't really said anything

2016-04-04 17:38
Josh Westlake
Well I doubt there is an emulator installed on that class computer. My laptop will surely crash if we try to do it on mine. I wonder if we can just take a screen recording and play it back in class. That might be the safest and easiest.
We could pause/play it as we narrate. The web part we could still do live.
If someone gets a sec maybe send Hadi an email about the screen recording thing and if we can do that or what his expectations are.

2016-04-04 17:41
Daniel J. Bouchard
He suggested an emulator when I talked to him, but I can't imagine him saying no to recording it.

2016-04-04 17:48
Josh Westlake
I just emailed him quickly. Also, we should remember to show off the WebAPI too, the SwaggerUI looks pretty slick.
Just another option if the demo comes up short timewise.

2016-04-04 17:49
Daniel J. Bouchard
*photo or sticker*

2016-04-04 17:52
Daniel J. Bouchard
https://onedrive.live.com/redir?resid=4F889267BA86EF00!20449&authkey=!ACSyjzfoNQt-jmI&ithint=file%2cpptx

2016-04-04 17:52
Daniel J. Bouchard
Here's the current state of the powerpoint.

2016-04-04 19:10
Josh Westlake
He said a video recording would be fine if we don't want to worry about the emulator.

2016-04-04 19:39
Josh Westlake
Does our Android app only fetch data from the cloud API or can it fetch data from the local machine? I'm wondering if I need to hold off on deploying the latest API updates.

2016-04-04 19:41
Daniel J. Bouchard
It only grabs info from the API

2016-04-04 19:42
Josh Westlake
Right, but you mean the AWS hosted API and not the local development machine's API correct?

2016-04-04 20:36
Josh Westlake
Well here's the issue. If I update the web front end with our new feature, we get the new API... which breaks the Android app. If we are demoing the Android app, we'll have to have a screen recording of it in action done BEFORE I deploy the API -- OR -- the Android app needs to be updated after the API update (which probably won't happen). So Daniel when you get a moment clarify for me what your current plan is for the Android app so I know if I'm going to be breaking anything for us.

2016-04-04 20:39
Daniel J. Bouchard
Okay. I'll get a demo video done soon.

2016-04-04 21:03
Daniel J. Bouchard
Video demo is done

2016-04-04 21:27
Josh Westlake
Okay, awesome.

2016-04-04 23:37
Josh Westlake
It looks like your presentation file was moved or something, if I try to access the link above it says there it might not exist or no longer available.

2016-04-04 23:38
Brent Rempel
Yeah I noticed that as well

2016-04-04 23:38
Matt Deutscher
wasn't sure if it was just me...

2016-04-04 23:42
Daniel J. Bouchard
try this one https://onedrive.live.com/redir?resid=4F889267BA86EF00!20454&authkey=!AI5YuqDfoeC6Tto&ithint=file%2cpptx

2016-04-04 23:43
Josh Westlake
That one works :)

2016-04-05 01:52
Josh Westlake
All the latest changes are live. I fixed the issue with checkboxes not saving and added the new suggestion feature which shows an additional list of items you can add to your list with a single click. It's enough for the presentation tomorrow at least.

2016-04-05 01:52
Daniel J. Bouchard
*photo or sticker*

2016-04-05 01:52
Josh Westlake
Now to start on the presentation for my other class...

2016-04-05 01:53
Daniel J. Bouchard
Ain't that lovely.

2016-04-06 23:47
Daniel J. Bouchard
Hey guys, any ideas why when I start a debug build on my local machine of the WebApp, I can't create a list?
Weird, I can't delete them either.

2016-04-06 23:49
Josh Westlake
What is the behavior after you click save or delete? Does the screen do nothing or redirect somewhere?

2016-04-06 23:49
Daniel J. Bouchard
It does nothing. The creating a list returned a method not accepted http status code
I also get an error on edit
Well, the screen reloads.
On the same page

2016-04-06 23:53
Josh Westlake
Do you have the newest version of the database? Specifically do you have an LASuggestion table?

2016-04-06 23:53
Daniel J. Bouchard
I just pulled from master, do I need to get that from somewhere else?

2016-04-06 23:55
Josh Westlake
Open the sql server object explorer is visual studio and open the ListAssist database. Check if it has an LASuggestion table in it. The project is set to rebuild the database if the model changes. I'm assuming it detects a change in model when a pull happens but maybe it didn't.

2016-04-06 23:55
Daniel J. Bouchard
That, however is a good suggestion since I have errors on list edit that have to do with that.

2016-04-06 23:57
Josh Westlake
If the WebAPI isn't properly talking to the DB then it will return an error code, and the webapp usually responds to error codes from the API by reloading the same page without any changes instead of redirecting somewhere or updating with changes.

2016-04-06 23:58
Daniel J. Bouchard
Well, I do have the LA suggestion table.
Is anyone else having the same issue?

2016-04-06 23:59
Josh Westlake
And you said your IDE is set to Build and not Release correct?

2016-04-06 23:59
Brent Rempel
I'll check right now

2016-04-06 23:59
Daniel J. Bouchard
It's set to debug

2016-04-06 23:59
Josh Westlake
or sorry, debug, okay that's fine

2016-04-07 00:00
Daniel J. Bouchard
I'll try release and see if that changes anything

2016-04-07 00:00
Josh Westlake
Release will break it

2016-04-07 00:00
Daniel J. Bouchard
Ah, kay

2016-04-07 00:00
Josh Westlake
Release is only meant for production
I'll delete my local repo and clone again and see what I get

2016-04-07 00:02
Daniel J. Bouchard
I almost had input validation done for creating lists and then I noticed this wired stuff.

2016-04-07 00:03
Josh Westlake
While I'm doing that have you tried to use the SwaggerUI to test the calls directly in the API?

2016-04-07 00:04
Brent Rempel
It's working fine on my machine

2016-04-07 00:04
Daniel J. Bouchard
Kay. I'll go try swagger.
Swagger is working fine.

2016-04-07 00:08
Josh Westlake
Oddly enough when I first ran the project with "ListAssist" set as startup it didn't work the first time, said "bad request". When I started up the WebAPI first once then went back to the primary project and started it everything worked fine. Maybe running the WebAPI first causes it to build some resource it needs.

2016-04-07 00:08
Josh Westlake
Try to run the primary project again.
Just curious.

2016-04-07 00:09
Daniel J. Bouchard
That would make me sad of that's how VS works.
Fuck off. It's working now. I'm sad, but thanks Josh!

2016-04-07 00:11
Josh Westlake
Np. It's probably something we could configure to work correctly every time, but might as well just use the workaround for now.

2016-04-07 14:24
Mark Cortilet
So I just ran into the issue Matt was talking about with not being able to add items to a list

2016-04-07 14:50
Josh Westlake
I am able to add items without issue directly from the currently deployed cloud API, which is what the android project is currently using. It might just require a tweak to how it's passing data back and forth from the API.

2016-04-07 14:50
Mark Cortilet
mmk, it's probably that or an emulator issue. thought i'd mention it though

2016-04-07 14:51
Josh Westlake
yep, always good to generate ideas :)

2016-04-07 15:10
Matt Deutscher
Sorry, running around with the kids right now. But last night, could delete items added from the Web api on the emulator just couldn't add items in android. I don't think it's the emulator, it worked before the changed api was pushed to AWS.

2016-04-07 15:21
Josh Westlake
The API changes we kept out of last iteration went in this iteration so chances are some adjustments have to be made.

2016-04-07 15:47
Mark Cortilet
mmk fixed adding items, but checking still needs work

2016-04-07 16:52
Mark Cortilet
and suggested item feature is done. can't really test it though since there aren't any entries in the db

2016-04-07 16:53
Daniel J. Bouchard
*photo or sticker*

2016-04-07 19:37
Josh Westlake
I added several suggestion items to the last list in the production database for testing the adding of suggestions to lists.

2016-04-07 19:39
Josh Westlake
I don't know if this works or not, but the Remote Desktop settings I use to access our production server are:
Computer Name: ec2-52-36-187-54.us-west-2.compute.amazonaws.com
User: Administrator
Password: Bm-BFJr7GDJ

I'm not 100% sure that's enough to access it but you can give it a try if you want to access the production database yourself. If it doesn't work I think the only workaround is for Matt to make you an account.

2016-04-07 20:28
Mark Cortilet
alright, everything looks good

2016-04-07 23:36
Matt Deutscher
Mark, could you push your latest to github?

2016-04-07 23:53
Mark Cortilet
should be in now

2016-04-08 00:39
Matt Deutscher
cool, add works great, but it doesn't delete items from lists now

2016-04-08 00:40
Mark Cortilet
Odd. I didn't touch delete

2016-04-08 00:41
Matt Deutscher
i think that's why it isn't working, was your change to add items an easy one? my guess is it would be about the same

2016-04-08 00:43
Mark Cortilet
It was just changing the call to the api from put to post
Delete already is set as delete

2016-04-08 00:45
Matt Deutscher
yeah, when you click the delete button, it goes away then comes back a second later. Did the delete call change at all in the web api?
check item doesn't work either

2016-04-08 00:52
Mark Cortilet
check is calling post when it should be put

2016-04-08 00:52
Matt Deutscher
yeah, trying something now
gradle is so slow...
fixed it, never mind
just had to change the way the put and delete were formatted

2016-04-08 00:57
Mark Cortilet
the url string?

2016-04-08 00:57
Matt Deutscher
yep, they work now. I'll p[usj my changes in a few minutes
*push

2016-04-08 00:58
Mark Cortilet
cool. was just about to try that

2016-04-08 01:08
Matt Deutscher
ok, it all works, tests pass
i'll push it now, then i have to study for my test later today...
