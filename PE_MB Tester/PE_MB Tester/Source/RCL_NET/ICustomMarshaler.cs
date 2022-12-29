/******************************************************
 *       Copyright Keysight Technologies 2018-2021
 ******************************************************/
using System;

namespace Keysight.ApiCoreLibraries
{
    /// <summary>
    /// This template interface define the methods/properties that a C# marshaler class should implement
    /// Every marshal class should implement this interface so that the marshaler can be composed by each other
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ICustomMarshaler<T>
    {
        /// <summary>
        /// return the buffer size (in bytes) that the type need for serializing, It is used to pre-allocate the memory for both passing data to the driver and getting data from the driver.
        /// for fixed size type, it is exactly the size of the object
        /// for variable size type, it should be the max size the object may be.
        /// </summary>
        Int32 BufferSize { get; }

        /// <summary>
        /// Takes a .NET type and writes it into the marshal buffer, advancing the count in the marshal buffer to the next available location.
        /// </summary>
        /// <param name="input">the .NET type object</param>
        /// <param name="marshalBuffer">the working marshal buffer</param>
        void CSharpToBytes(T input, MarshalBuffer marshalBuffer);

        /// <summary>
        /// Takes a marshal buffer and reads out a .NET type. It advances the count in the buffer to the next location
        /// </summary>
        /// <param name="marshalBuffer">the working marshal buffer</param>
        /// <param name="result">the output .NET object</param>
        void BytesToCSharp(MarshalBuffer marshalBuffer, ref T result);
    }
}