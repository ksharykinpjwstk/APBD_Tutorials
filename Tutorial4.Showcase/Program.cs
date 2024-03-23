using Tutorial4.Showcase.Liskov.Broken;
using Tutorial4.Showcase.Liskov.Sticked;
using Tutorial4.Showcase.OpenClosed.Broken;
using Tutorial4.Showcase.OpenClosed.Sticked;
using Tutorial4.Showcase.SingleResponsobility;
using AreaCalculatorService = Tutorial4.Showcase.OpenClosed.Broken.AreaCalculatorService;
using Rectangle = Tutorial4.Showcase.OpenClosed.Broken.Rectangle;

/* Single responsibility
 * Class should be responsible for one and only one business logic
 */
GovernmentService governmentService = new();
//WTF???
governmentService.GeneratePassport();

SingleAdminstrativeOfficeService adminstrativeOfficeService = new();
// LIKE 👍
adminstrativeOfficeService.GeneratePassport();

//Open closed principle
//Opened for extension, closed for modification
Rectangle rectangleOne = new (){Width = 2.3, Height = 3.5};
Rectangle rectangleTwo = new (){Width = 4.0, Height = 5.1};
Circle aloneCircle = new() { Radius = 10 };
AreaCalculatorService calculatorService = new();
Console.WriteLine(calculatorService.CalculateSum([rectangleOne, rectangleTwo]));
//WTF???
//calculatorService.CalculateSum([rectangleOne, aloneCircle]);

List<Tutorial4.Showcase.OpenClosed.Sticked.Rectangle> rectangles =
[
    new Tutorial4.Showcase.OpenClosed.Sticked.Rectangle { Height = 5, Width = 8 },
    new Tutorial4.Showcase.OpenClosed.Sticked.Rectangle { Height = 10, Width = 10 }
];

List<Triangle> triangles =
[
    new Triangle { Base = 10, Height = 5 },
    new Triangle { Base = 12, Height = 6 }
];

NewAreaCalculatorService newAreaCalculatorService = new();
var totalTrianglesArea = newAreaCalculatorService.CalculateSum(triangles.ToArray());
var totalRectanglesArea = newAreaCalculatorService.CalculateSum(rectangles.ToArray());

/* LISKOV PRINCIPLE
 *
 * If a class S is a subclass of class T, an object of class T should be replaceable by an object of class S without
 * altering the desirable properties of the program.
 *
 * SIMPLY:
 *
 * Your inherited class should stick with behaviour of your parent class.
 *
 * Useful link: https://stackoverflow.com/questions/56860/what-is-an-example-of-the-liskov-substitution-principle
 */

RestrictedFileWorker fileWorker = new("123.txt");
fileWorker.ReadFile();
// Exception, WTF???
fileWorker.WriteFile();

// LIKE 👍
NewRestrictedFileWorker newRestrictedFileWorker = new();
// Only reading file is available
newRestrictedFileWorker.ReadFile("321.txt");