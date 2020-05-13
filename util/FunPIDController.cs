using System;

public
class FunPIDController: PIDController
{
	private readonly Func<double> getter;
	private readonly Action<double> setter;

	public FunPIDController(double Kp, double Ki, double Kd, Func<double> getter, Action<double> setter)
		:base(Kp, Ki, Kd)
	{
		this.getter = getter;
		this.setter = setter;
	}

	public void run(double deltaT)
	{
		setter(run(deltaT, getter()));
	}
}
