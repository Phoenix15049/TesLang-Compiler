namespace TesLang_Compiler;

using System;
using System.Collections.Generic;

public abstract class AstNode { }

public class ProgramNode : AstNode
{
    public List<FunctionNode> Functions { get; } = new List<FunctionNode>();
}

public class FunctionNode : AstNode
{
    public string Name { get; }
    public List<ParameterNode> Parameters { get; } = new List<ParameterNode>();
    public string ReturnType { get; }
    public List<StatementNode> Body { get; } = new List<StatementNode>();

    public FunctionNode(string name, string returnType)
    {
        Name = name;
        ReturnType = returnType;
    }
}

public class ParameterNode : AstNode
{
    public string Name { get; }
    public string Type { get; }

    public ParameterNode(string name, string type)
    {
        Name = name;
        Type = type;
    }
}

public abstract class StatementNode : AstNode { }

public class VariableDeclarationNode : StatementNode
{
    public string Name { get; }
    public string Type { get; }
    public ExpressionNode InitialValue { get; }

    public VariableDeclarationNode(string name, string type, ExpressionNode initialValue)
    {
        Name = name;
        Type = type;
        InitialValue = initialValue;
    }
}

public class ReturnStatementNode : StatementNode
{
    public ExpressionNode Expression { get; }

    public ReturnStatementNode(ExpressionNode expression)
    {
        Expression = expression;
    }
}

public abstract class ExpressionNode : AstNode { }

public class IdentifierNode : ExpressionNode
{
    public string Name { get; }

    public IdentifierNode(string name)
    {
        Name = name;
    }
}

public class NumberNode : ExpressionNode
{
    public int Value { get; }

    public NumberNode(int value)
    {
        Value = value;
    }
}

public class BinaryExpressionNode : ExpressionNode
{
    public ExpressionNode Left { get; }
    public string Operator { get; }
    public ExpressionNode Right { get; }

    public BinaryExpressionNode(ExpressionNode left, string op, ExpressionNode right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
}

public class FunctionCallNode : ExpressionNode
{
    public string FunctionName { get; }
    public List<ExpressionNode> Arguments { get; } = new List<ExpressionNode>();

    public FunctionCallNode(string functionName)
    {
        FunctionName = functionName;
    }
}
