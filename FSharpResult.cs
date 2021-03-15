[CompilationMapping(SourceConstructFlags.SumType)]
    [CompiledName("FSharpResult`2")]
    [Struct]
    [StructuralComparison]
    [StructuralEquality]
    public struct FSharpResult<T, TError> : IEquatable<FSharpResult<T, TError>>, IStructuralEquatable, IComparable<FSharpResult<T, TError>>, IComparable, IStructuralComparable
    {
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DebuggerNonUserCode]
        public int Tag { get; }
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DebuggerNonUserCode]
        public bool IsOk { get; }
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [DebuggerNonUserCode]
        public bool IsError { get; }
        [CompilationMapping(SourceConstructFlags.Field, 0, 0)]
        [CompilerGenerated]
        [DebuggerNonUserCode]
        public T ResultValue { get; }
        [CompilationMapping(SourceConstructFlags.Field, 1, 0)]
        [CompilerGenerated]
        [DebuggerNonUserCode]
        public TError ErrorValue { get; }

        [CompilationMapping(SourceConstructFlags.UnionCase, 1)]
        public static FSharpResult<T, TError> NewError(TError errorValue);
        [CompilationMapping(SourceConstructFlags.UnionCase, 0)]
        public static FSharpResult<T, TError> NewOk(T resultValue);
        [CompilerGenerated]
        public sealed override int CompareTo(FSharpResult<T, TError> obj);
        [CompilerGenerated]
        public sealed override int CompareTo(object obj);
        [CompilerGenerated]
        public sealed override int CompareTo(object obj, IComparer comp);
        [CompilerGenerated]
        public sealed override bool Equals(object obj, IEqualityComparer comp);
        [CompilerGenerated]
        public sealed override bool Equals(FSharpResult<T, TError> obj);
        [CompilerGenerated]
        public sealed override bool Equals(object obj);
        [CompilerGenerated]
        public sealed override int GetHashCode(IEqualityComparer comp);
        [CompilerGenerated]
        public sealed override int GetHashCode();

        public static class Tags
        {
            public const int Ok = 0;
            public const int Error = 1;
        }
    }
}