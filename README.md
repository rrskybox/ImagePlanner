# ImagePlanner
Windows 10 Astro-imaging Planning Tool utilizing TheSkyX

Overview

Image Planner is a Windows application that generates calendars to forecast daily object imaging availability for a given astronomical object and year.  The application utilizes TSX for basic object and location information.  Image Planner 3.0 is a follow-on to Image Forecast 2.0.  The latter version added two new windows: A target altitude window that displays the altitude of the target over the selected night and information about the moon position, and a is a target image window that shows the DSS2 image (uploaded from www.sky-map.org) of the target inside a box which represents the user’s currently active imager FOV in TheSkyX.  This third version adds yet another pop-up window for selecting potential targets on whichever date has been selected.
 
In each daily entry, an imaging start time and duration is displayed along with an indicator for the moon phase during that period.  The imaging start time and duration is the time the object is above the minimum altitude and bracketed by astronomical twilight.  The moon phase is indicated by a “O:” surrounded by 0 to 4 parentheses.  A “O” indicates a new moon; a “((((O))))” indicates a full moon.
The color of the date entry represents the percentage of time that the moon is above the horizon during the computed imaging period.  The color ranges from dark blue (0%) to yellow (100%).  Gray indicates that the object does not rise above the minimum altitude during available darkness.  

An asterisk prefaces imaging time entries that are split – meaning that two imaging periods are available that night (i.e. the object dips below the minimum altitude during the night.).  The application automatically chooses the longest of those two periods for display.  Hovering the cursor over any entry will display additional details for imaging the object on that date.  Selecting (clicking) on the date initiates the altitude and preview information for that target.

Controls

Up and Down Arrows: Increment (or decrement) the object number for the specific catalog refenced, e.g. NGC450 -> NGC451.

Create: Calendars can be generated after entering a new object or year, or by selecting the “Create” button, based on the current Target entry.  

Details: The Details command generates a small pop up window that contains the target details as generated by TSX.  This is the same information as presented when hovering over a particular date, but it doesn’t disappear in 7 seconds. 
 
Altitude: The Altitude command creates a pop-up window that graphs the altitude of the target (red) and of the moon (yellow) for the period of time that the target is visible from the user’s location.  Additional imaging information is presented at the bottom of this window.

Track: The Track command creates a pop-up window that shows an orthographic projection of the night of the selected date, centered on the zenith at midnight, north up, with dusk to left and dawn to the right.  The path of the target during this period is drawn in red.  The path of the moon, if up, is drawn in yellow.  The target culmination (transit) is drawn in orange.

Preview: The Preview command generates a pop up window that shows a full color image of the target as downloaded from www.Sky-Map.com.  This image is from the Sloan Digital Sky Survey 2 (SDD2) database.  Superimposed on the image will be an FOV indicator representing the user’s currently active TSX FOVI as specified in their TSX FieldOfView.txt file.  The FOVI is moderately accurate, but don’t take it to the bank.

Minimum Altitude: Lowest altitude that Image Analyzer will use for estimating the time a target is available.

Current Target List/Add/Remove: If the user wishes to preserve any target for replay, the Add button will cause a small XML file to be created in a directory called NightShift in the user’s Document folder using the current Target name.  These targets can be retrieved using the Current Target List drop down.  To get rid of a target, use the Remove button.

Print: The calendar and any pop-up located in front of the calendar area are printed to the user’s default printer.  If just the calendar is wanted, close, or move to the side, all pop-ups.

Close: Just what it sounds like – same as the “X”.

Prospects:  Prospects generates pop-up window in the lower middle of the application window for reviewing objects that are visible between dusk and dawn on the month and day selected.  Upon launch, the user selects the type of target to search for (galaxies, clusters, nebula).  TSX then creates an Observing List for that class of target and the object list is downloaded.  The user then selects (double click) on the type of object to be displayed from a list of object types found by TSX.  These objects are displayed in a table to the right.  The table displays the object name, object size, duration (visible above minimum altitude) and maximum altitude (while visible).  The object table can be sorted alphabetically or numerically by double clicking on any of the headers.   The minimum and maximum values for size, duration and minimum altitude are also computed and displayed for culling the object list.  Changing any minimum will reduce the list to only those objects with values above that minimum.  

A target can be selected by double clicking on any object name.  At the time, the monthly calendar and any open pop-up windows will be updated to show information about that target for the year and the selected day.  If the user selects a different day from the calendar, however, the Prospect window will be closed as its data is no longer valid for the new date.  Clicking on the Prospects button when the pop-up is open will also close the window.

Errata

In the interests of program speed, times are approximate, but generally accurate within five to ten minutes or so.   Accordingly, this application is intended solely as a planning tool for selecting optimal imaging days for a given object.  Once a day has been selected for imaging an object, then highly accurate, and more user-friendly, imaging tools like CCD Navigator are more appropriately applied.  Note that these tools have the added benefit of actually having some timely and knowledgeable customer support.

The pop up windows are initial located at the corners of the main window, but can be moved where ever on the screen.  After closing a pop up, it will return its last location when reopened.  The pop up windows always remain on top of the main window.

Another limitation is that, although the application uses TSX to set the observer’s location, calendars are generated based on the current PC time.  An attempt to generate a calendar for a TSX location that is not in your current time zone will produce unpredictable results.  Lastly, the application does not work for solar system objects.

For saving targets in the target list, a directory will be created in the user’s Documents directory called NightHawk.  When a target is saved, a new XML file will be created in this folder.  When a target is removed from the list, the file will be deleted.  The file contains only the target name and position information.   A user can add additional XML elements so long as the original XML elements are not messed with. 

The Prospect pop-up relies on three TSX queries that are copied to the SB Database Queries folder upon launch, if they aren't previously installed.  These queries are named: "ImagePlannerGalaxy.dbq", "ImagePlannerCluster.dbq", and "ImagePlannerNebula.dbq".  Once installed, any of these object list queries can be modified and saved.  Once you start modifing them, however, you're on your own.  If the queries are deleted, Image Planner will reinstall any or all of them during its next run.

Generating a Prospect query is CPU-intensive.  The process can take 15 or 20 seconds to compile and process a long list.  The easiest way to radically decrease overall processing time is to reduce the Target Update Rate under Advanced Preferences to 1 or 2 (per sec). The time can also be shortened by limiting the number of objects that a TSX query can produce (Edit -> Preferences -> Advanced Preferences -> Maximum number of objects in observing list).  Another way is to limit a query to a greater minimum altitude (default is 10 degrees).  To change the minimum altitude, open the query in TSX, edit the Filter value for Minimum Altitude, and save the query back to the orginal filename.  Of course, improving the responsiveness will reduce the number and variety of objects to consider.  Your choice.

Requirements

Image Planner is a Windows Forms executable, written in Visual Basic.  The app requires TheSkyX Pro (Build 11360 or later).  The application runs as an uncertified, standalone application under Windows 7, 8 and 10.  

Microsoft.VisualBasic.PowerPacks.Vs.version 9.0.0.0 installation may be required.  This software can be found at:
https://www.microsoft.com/en-us/download/details.aspx?id=25169

For more PowerPack information, see: https://msdn.microsoft.com/en-us/library/bb882689.aspx

Installation

Download the ImagePlanner_Exe.zip and open. Run the "Setup" application.  Upon completion, an application icon will have been added to the start menu under the category "TSXToolKit" with the name "Image Planner".  This application can be pinned to Start or Desktop if desired.  

Support 

This application was written for the public domain and as such is unsupported. The developer wishes you his best and hopes everything works out, but recommends learning C# (it's really not horrible and the tools are free from Microsoft) if you find a problem or want to add features.  The source is supplied as a Visual Studio project.

Acknowledgments

Some of the methods in this application derive directly from concepts, algorithms and programs found in Astronomy on the Personal Computer 4th Edition, Oliver Montenbruck and Thomas Pfleger, Springer, 2000.  Great book; much appreciated.

The target image is generated through an API provided by www.Sky-Map.org to its servers.  Use of this API is free to non-commercial users.  Obviously, this application was written as and expects to remain as non-commercial.
    
