using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace X_Util.NeuralNetwork
{
	public class NeuralNet
	{
		public double LearnRate { get; set; }
		public double Momentum { get; set; }
		public List<Neuron> InputLayer { get; set; }
		public List<Neuron> HiddenLayer { get; set; }
		public List<Neuron> OutputLayer { get; set; }

		private static readonly System.Random Random = new System.Random();

        /// <summary>
        /// initialize
        /// </summary>
        /// <param name="inputSize">indicates how many neurons are in the input layer</param>
        /// <param name="hiddenSize">indicates how many neurons are in the hidden layer</param>
        /// <param name="outputSize">indicates how many neurons are in the output layer</param>
        /// <param name="learnRate">(optional)the global learning rate of the network</param>
        /// <param name="momentum">(optional) the initial momentum</param>
		public NeuralNet(int inputSize, int hiddenSize, int outputSize, double? learnRate = null, double? momentum = null)
		{
			LearnRate = learnRate ?? .4;
			Momentum = momentum ?? .9;
			InputLayer = new List<Neuron>();
			HiddenLayer = new List<Neuron>();
			OutputLayer = new List<Neuron>();

			for (int i = 0; i < inputSize; i++)
				InputLayer.Add(new Neuron());

			for (int i = 0; i < hiddenSize; i++)
				HiddenLayer.Add(new Neuron(InputLayer));

			for (int i = 0; i < outputSize; i++)
				OutputLayer.Add(new Neuron(HiddenLayer));
		}

        /// <summary>
        /// Train the neural network by given dataset and epoch number
        /// </summary>
        /// <param name="dataSets">the dataset containing values and targets</param>
        /// <param name="numEpochs">how many times repeat training on it</param>
		public void Train(List<DataSet> dataSets, int numEpochs)
		{
			for (int i = 0; i < numEpochs; i++)
			{
				foreach (DataSet dataSet in dataSets)
				{
					ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
				}
			}
		}

        /// <summary>
        /// Train the neural network by given dataset and minimum error
        /// </summary>
        /// <param name="dataSets">the dataset containing values and targets</param>
        /// <param name="minimumError">training ends the error is lower than this amount</param>
		public void Train(List<DataSet> dataSets, double minimumError)
		{
            double error = 1.0;
			int numEpochs = 0;

			while (error > minimumError && numEpochs < int.MaxValue)
			{
				var errors = new List<double>();
				foreach (DataSet dataSet in dataSets)
				{
					ForwardPropagate(dataSet.Values);
					BackPropagate(dataSet.Targets);
					errors.Add(CalculateError(dataSet.Targets));
				}
				error = errors.Average();
				numEpochs++;
			}
		}

        /// <summary>
        /// Feed the input to the network and propagate forwards
        /// </summary>
        /// <param name="inputs">the feeded input data</param>
		private void ForwardPropagate(params double[] inputs)
		{
            int i = 0;
			InputLayer.ForEach(p => p.Value = inputs[i++]);
			HiddenLayer.ForEach(p => p.CalculateValue());
			OutputLayer.ForEach(p => p.CalculateValue());
		}

        /// <summary>
        /// Use the target output to adjust the network and propagate backwards
        /// </summary>
        /// <param name="targets">the feeded input data</param>
		private void BackPropagate(params double[] targets)
		{
			int i = 0;
			OutputLayer.ForEach(p => p.CalculateGradient(targets[i++]));
			HiddenLayer.ForEach(p => p.CalculateGradient());
			HiddenLayer.ForEach(p => p.UpdateWeights(LearnRate, Momentum));
			OutputLayer.ForEach(p => p.UpdateWeights(LearnRate, Momentum));
		}

        /// <summary>
        /// Compute the data by the given inputs, feed forward, and get output values
        /// </summary>
        /// <param name="inputs">the given input data</param>
        /// <returns></returns>
		public double[] Compute(params double[] inputs)
		{
			ForwardPropagate(inputs);
			return OutputLayer.Select(p => p.Value).ToArray();
		}

        /// <summary>
        /// Calculate the sum of errors from output
        /// </summary>
        /// <param name="targets">the target value to compare</param>
        /// <returns></returns>
		private double CalculateError(params double[] targets)
		{
			int i = 0;
			return OutputLayer.Sum(p => Mathf.Abs((float)p.CalculateError(targets[i++])));
		}

		public static double GetRandom()
		{
			return 2.0 * Random.NextDouble() - 1.0;
		}
	}

	public enum TrainingType
	{
		Epoch,
		MinimumError
	}

}
