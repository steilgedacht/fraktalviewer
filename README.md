# Fraktalviewer

A program to explore fractals like the mandelbrot-set and the corresponding julia-sets.

The main program is the `Fraktalviewer.exe`.
The project was made in Visual Studio, the corresponding project files are in the [/Fraktalviewer](https://github.com/steilgedacht/fraktalviewer/tree/main/Fraktalviewer) directory.

![Bild1](https://user-images.githubusercontent.com/89748204/155360978-ba80e03a-d8f0-44be-9e76-141ffc999b51.PNG)

**I made also a couple of explainitory videos:**  
Mandelbrot set: https://www.youtube.com/watch?v=_78kGnsYXLc  
Other fractals: https://www.youtube.com/watch?v=HFXle1n3aO8  
Julia set: https://www.youtube.com/watch?v=lkrARpExZkc  

## Navigation Tools


### Buttons
![image](https://user-images.githubusercontent.com/89748204/155361418-85f21e8c-b685-4a2d-89a5-ee029c275d2f.png)<br/>
To select the Mandelbortset (left picture) press the M Key or click on the Mandelbrotmenge button.<br/>
To select the Juliaset (right picture) press the J Key or click on the Juliamenge button.<br/>
To zoom out or in in the selected set, you can use these 2 Buttons ![image](https://user-images.githubusercontent.com/89748204/155361940-f0847bcd-c76f-422d-8642-e93e346ce1ff.png) <br/>
Refresh the view with ![image](https://user-images.githubusercontent.com/89748204/155362042-8bf9650f-7bf0-481f-a345-a855509bb7b1.png)

### Dynamic View

You can enalbe dynamic view with this button: ![image](https://user-images.githubusercontent.com/89748204/155363161-f8b21631-c3fd-4a7c-af80-3629ea6eacaf.png)

Here you first have to expand the window in the bottom right with ![image](https://user-images.githubusercontent.com/89748204/155362520-e1879969-ac9a-4372-aca4-b4c732f00cf0.png)
The dynamic view has only a limited size and a limited iteration depth in order to increase refresh time: <br/>
![image](https://user-images.githubusercontent.com/89748204/155362373-2918ead6-bd50-4e25-9c01-171532c78b48.png)

## Underlying Formulas
There are several different formulas to choose:<br/>
![image](https://user-images.githubusercontent.com/89748204/155363332-bb5e29b6-96b7-417a-a275-0eb4859a03ab.png)


## Color Themes
There are also multiple color themes avaidble:<br/>
![image](https://user-images.githubusercontent.com/89748204/155363415-74d1c211-101b-4af0-af7c-78eb67744130.png)

You can also change the black color of the elements, that are contained in the set, to a different color. <br/>
![image](https://user-images.githubusercontent.com/89748204/155363475-55dfcb00-b0af-41ae-8ead-8d1598db2880.png)


## Additional Data
![image](https://user-images.githubusercontent.com/89748204/155363534-2daea228-1762-4cd0-bb45-9697ceb688c6.png)

## Exporting Images
![image](https://user-images.githubusercontent.com/89748204/155363569-fb7f68b9-1fd8-40c2-b728-029e212ddeb3.png) <br/>
You can add a name for the file in the "Dateiname" box and the click on "Speichern" to save the image. Select the Mandelbrotset or the Juliaset first before saving.

## Animation
By clicking on Animationsbereich you can configure a starting position, a ending position and the steps inbetween them. <br/>
![image](https://user-images.githubusercontent.com/89748204/155363864-cac02a16-44af-427a-a4d6-051e11994805.png)<br/>
This configuration would mean: 
Start at 1 + i1 and go to 2 + i2 in 5 steps.
If you then click on Animate, the images get exported and you can later put them together to a video in a software like Blender.
![image](https://user-images.githubusercontent.com/89748204/155364336-f854569d-93ea-41d3-a7c4-bb4e2fa9257d.png)

