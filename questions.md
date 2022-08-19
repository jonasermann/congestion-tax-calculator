The assignment had three overall tasks, namely

* The application currently doesn't have an entry point, add a way to call the calculation with different inputs, preferrably over HTTP.
* As stated there may be bugs in the code, try to find and fix them.
* There is no particular structure to the code so there are several improvements that can be made.

While I understood the second task, I don't think I really understood the first one. The only way I can see creating an entry point over HTTP 
was to create a seperate webAPI project, which references the class library, but I'm not sure if that's what you wanted me to do. Furthermore, 
I was not able to pass on objects that implement the Vehicle interface over HttpPost calls, so instead I simply passed on a string which if 
equal to the return value of GetVehicleType() of any existing class that implements the Vehicle interface passes on that class to the
CongestionTaxCalculator (I do so by having a dictionary in the controller with strings as keys and classes as values). My question is, thus,
what you meant by creating an entry point over Http. 
