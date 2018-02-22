using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace X_Util.NeuralNetwork
{
    /// <summary>
    /// Represents a neuron cell
    /// </summary>
	public class Neuron 
	{
		public List<Synapse> InputSynapses { get; set; }
		public List<Synapse> OutputSynapses { get; set; }
		public double Bias { get; set; }
		public double BiasDelta { get; set; }
		public double Gradient { get; set; }
		public double Value { get; set; }

        /// <summary>
        /// Empty Constructor
        /// </summary>
		public Neuron()
		{
			InputSynapses = new List<Synapse>();
			OutputSynapses = new List<Synapse>();
			Bias = NeuralNet.GetRandom();
		}

        /// <summary>
        /// Make a new neuron that takes synapse from all given input neurons
        /// </summary>
        /// <param name="inputNeurons"></param>
		public Neuron(IEnumerable<Neuron> inputNeurons) : this()
		{
			foreach (var inputNeuron in inputNeurons)
			{
				var synapse = new Synapse(inputNeuron, this);
				inputNeuron.OutputSynapses.Add(synapse);
				InputSynapses.Add(synapse);
			}
		}

        /// <summary>
        /// Calculates and updates the current value stored in this neuron, by taking the output value of input neurons mutiplied by their weight, plus a bias
        /// </summary>
        /// <returns>the calculated value</returns>
		public virtual double CalculateValue()
		{
			return Value = Sigmoid.Output(InputSynapses.Sum(a => a.Weight * a.InputNeuron.Value) + Bias);
		}

        /// <summary>
        /// Calculates the error, the difference between output value and target value
        /// </summary>
        /// <param name="target">the target value</param>
        /// <returns></returns>
		public double CalculateError(double target)
		{
			return target - Value;
		}

        /// <summary>
        /// Calculate the gradient value of this neuron, by multiply the error with the derivative
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
		public double CalculateGradient(double? target = null)
		{
			if(target == null)
				return Gradient = OutputSynapses.Sum(a => a.OutputNeuron.Gradient * a.Weight) * Sigmoid.Derivative(Value);

			return Gradient = CalculateError(target.Value) * Sigmoid.Derivative(Value);
		}

        /// <summary>
        /// Update the weights by given momentum and learning rate.
        /// Uses gradient descent to adjust the weight
        /// </summary>
        /// <param name="learnRate">How much it learns from the momentum, [0, 1]</param>
        /// <param name="momentum"></param>
		public void UpdateWeights(double learnRate, double momentum)
		{
			var prevDelta = BiasDelta;
            // update the bias using the gradient
            BiasDelta = learnRate * Gradient;
			Bias += BiasDelta + momentum * prevDelta; 

			foreach (var synapse in InputSynapses)
			{
                // update each synapse by their partial gradient (multiply the neuron gradient by the input value)
				prevDelta = synapse.WeightDelta;
				synapse.WeightDelta = learnRate * Gradient * synapse.InputNeuron.Value; 
				synapse.Weight += synapse.WeightDelta + momentum * prevDelta;
			}
		}

	}

    /// <summary>
    /// Represents a synapse connection between neuron layers
    /// </summary>
	public class Synapse
	{
		public Neuron InputNeuron { get; set; }
		public Neuron OutputNeuron { get; set; }
		public double Weight { get; set; }
		public double WeightDelta { get; set; }

		public Synapse(Neuron inputNeuron, Neuron outputNeuron)
		{
			InputNeuron = inputNeuron;
			OutputNeuron = outputNeuron;
			Weight = NeuralNet.GetRandom();
		}
	}

    /// <summary>
    /// Sigmoid function (1/(1+e^-x)), and its partial derivative
    /// </summary>
	public static class Sigmoid
	{
		public static double Output(double x)
		{
			return x < -45.0 ? 0.0 : x > 45.0 ? 1.0 : 1.0 / (1.0 + Mathf.Exp((float)-x));
		}

		public static double Derivative(double x)
		{
			return x * (1 - x); // f'(x) = f(x)(1-f(x))
		}
	}

    /// <summary>
    /// Represents a dataset consists of a set of values and target result of output
    /// </summary>
	public class DataSet
	{
		public double[] Values { get; set; }
		public double[] Targets { get; set; }

		public DataSet(double[] values, double[] targets)
		{
			Values = values;
			Targets = targets;
		}
	}

}
