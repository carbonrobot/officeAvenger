/* Based on this.Log() by Rob Reynolds
 * http://devlicio.us/blogs/rob_reynolds/archive/2012/12/15/introducing-this-log.aspx
 *
 * Logging is based on a lazy logging approach, that is, if no logger has been configured 
 * then the logging calls are never executed.
 *
 * This approach allows better performance by caching the available loggers and providing a single 
 * interface regardless of the actual logging implementation.
 *
 * Logging is currently configured to log to the users "Documents/ProMap/logs" directory
 */

// To enable logging in any class/file, reference this assembly/project and add the following using statement
using Services.Logging;

// An example of how to log an exception from a try catch
public void MethodThatThrowsException()
{
	try
	{
		var x = 15 / 0;
	}
	catch (Exception ex)
	{
		this.Log().Error(() => ex.ToString());
	}
}

// An example of how to log information for debugging or troubleshooting
public void ComplexMethod(int samples)
{
	this.Log().Info(() => "Starting complex method");
	
	if(samples < 2)
		this.Log().Warn(() => "We need at least 2 samples, but this wont break anything");
		
	this.Log().Info(() => "End of complex method");
}

// An example of formatting to make debugging easier
public void FormattedDebugMessages(double[] positions)
{
	this.Log().Info(() => "Starting with {0} positions".FormatWith(positions.Length));
	
	var first = positions.First();
	var last = positions.Last();
	this.Log().Info(() => "The first number is {0} and the last is {1}".FormatWith(first, last));
}

// logging levels available, listed from normal to most problematic
this.Log().Debug(() => "Detailed information about the programs normal operation, typically turned off until needed.");
this.Log().Info(() => "Interesting but normal runtime events such as starting and stopping services or hardware.");
this.Log().Warn(() => "Unexpected conditions that still allow the user to continue normally.");
this.Log().Error(() => "Unexpected conditions that prevent the user from continuing normally and may result in data loss.");
this.Log().Fatal(() => "Severe errors that cause application termination or failure.");