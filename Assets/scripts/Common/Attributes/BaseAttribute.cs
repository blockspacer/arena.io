﻿
namespace Attributes
{
    public class BaseAttribute<T>
    {
        protected float value_;
        protected float percentValue_;
        protected float multiplier_;
        protected T id_;

        public BaseAttribute()
        {
            value_ = 0.0f;
            multiplier_ = 0.0f;
            percentValue_ = 0.0f;
        }

        public BaseAttribute<T> SetValue(float value)
        {
            value_ = value;
            CalculateValue ();
            return this;
        }

        public BaseAttribute<T> SetMultiplier(float m)
        {
            multiplier_ = m;
            CalculateValue();
            return this;
        }

        public BaseAttribute<T> SetPercentValue(float v)
        {
            percentValue_ = v;
            CalculateValue();
            return this;
        }

        public BaseAttribute<T> ModifyValue(float value)
        {
            value_ += value;
            CalculateValue ();
            return this;
        }

        public T ID
        { 
            get { return id_; }
        }

        public float Value
        {
            get { return value_; }
        }

        public float PercentValue
        {
            get { return percentValue_; }
        }

        public float Multipler
        {
            get { return multiplier_; }
        }

        public virtual void CalculateValue ()
        {}
    }
}