public
class PIDController
{
	private readonly double Kp;
	private readonly double Ki;
	private readonly double Kd;

	private double sumError;
	private double lastError;

	public PIDController(double Kp, double Ki, double Kd)
	{
		this.Kp = Kp;
		this.Ki = Ki;
		this.Kd = Kd;

		sumError = 0;
		lastError = 0;
	}

	public double run(double deltaT, double error)
	{
		var deltaError = (error - lastError) / deltaT;
		sumError += error * deltaT;
		lastError = error;

		return Kp*error + Ki*sumError + Kd*deltaError;
	}
}
