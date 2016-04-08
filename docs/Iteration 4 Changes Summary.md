#Iteration 4 - Summary of Changes

This document is to assist the marker in identifying changes between this iteration and the last.

##What's New

* Our focus this iteration was bug fixing and adding as many new features as we could (which wasn't many, sorry)
  * Fixed the issue with checkboxes not saving in the web app (finally) [[Issue #54](https://github.com/DailyDilemma/COMP4350/issues/54)]
  * Implemented the suggestions feature in both the web app and on Android. [[Issue #55](https://github.com/DailyDilemma/COMP4350/issues/55)]
    * NOTE: The suggestions feature was closely related to the replacement intervals feature (which we could not finish). What this means is that the system can work with suggestions that already exist, but can't make new suggestions (that's what the replacement intervals are supposed to do). I will populate the database with many sample suggestions when deploying to production so you have lots to work with, but once you accept a suggestion the suggestion is consumed permanently as there is no feature to generate new suggestions.
  * Merged in all of the refactored code for the WebAPI that was kept out last iteration due to lack of time to write tests for it.
  
##What Didn't Make It In

* We were only able to add one additional feature this iteration, so we are way short of the 9-12 we should have. Be gentle.
