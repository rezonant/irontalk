/*
 * SmalltalkAnalyzer.cs
 *
 * THIS FILE HAS BEEN GENERATED AUTOMATICALLY. DO NOT EDIT!
 *
 * Permission is granted to copy this document verbatim in any
 * medium, provided that this copyright notice is left intact.
 *
 * Copyright © 2010 William Lahti
 */

using PerCederberg.Grammatica.Runtime;

/**
 * <remarks>A class providing callback methods for the
 * parser.</remarks>
 */
public abstract class SmalltalkAnalyzer : Analyzer {

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public override void Enter(Node node) {
        switch (node.Id) {
        case (int) SmalltalkConstants.IDENT:
            EnterIdent((Token) node);
            break;
        case (int) SmalltalkConstants.KEYWORD:
            EnterKeyword((Token) node);
            break;
        case (int) SmalltalkConstants.SELECTOR:
            EnterSelector((Token) node);
            break;
        case (int) SmalltalkConstants.STRING:
            EnterString((Token) node);
            break;
        case (int) SmalltalkConstants.CHAR_LITERAL:
            EnterCharLiteral((Token) node);
            break;
        case (int) SmalltalkConstants.NUM_LITERAL:
            EnterNumLiteral((Token) node);
            break;
        case (int) SmalltalkConstants.HASH:
            EnterHash((Token) node);
            break;
        case (int) SmalltalkConstants.LEFT_PAREN:
            EnterLeftParen((Token) node);
            break;
        case (int) SmalltalkConstants.RIGHT_PAREN:
            EnterRightParen((Token) node);
            break;
        case (int) SmalltalkConstants.LEFT_BRACKET:
            EnterLeftBracket((Token) node);
            break;
        case (int) SmalltalkConstants.RIGHT_BRACKET:
            EnterRightBracket((Token) node);
            break;
        case (int) SmalltalkConstants.LEFT_BRACE:
            EnterLeftBrace((Token) node);
            break;
        case (int) SmalltalkConstants.RIGHT_BRACE:
            EnterRightBrace((Token) node);
            break;
        case (int) SmalltalkConstants.COLON:
            EnterColon((Token) node);
            break;
        case (int) SmalltalkConstants.SEMI_COLON:
            EnterSemiColon((Token) node);
            break;
        case (int) SmalltalkConstants.VAR_DELIM:
            EnterVarDelim((Token) node);
            break;
        case (int) SmalltalkConstants.DOT:
            EnterDot((Token) node);
            break;
        case (int) SmalltalkConstants.BINARY:
            EnterBinary((Token) node);
            break;
        case (int) SmalltalkConstants.ASSIGN:
            EnterAssign((Token) node);
            break;
        case (int) SmalltalkConstants.SEQUENCE:
            EnterSequence((Production) node);
            break;
        case (int) SmalltalkConstants.STATEMENT:
            EnterStatement((Production) node);
            break;
        case (int) SmalltalkConstants.SYMBOL_LITERAL:
            EnterSymbolLiteral((Production) node);
            break;
        case (int) SmalltalkConstants.VAR_DEF:
            EnterVarDef((Production) node);
            break;
        case (int) SmalltalkConstants.BLOCK_PARAMS:
            EnterBlockParams((Production) node);
            break;
        case (int) SmalltalkConstants.BLOCK_LITERAL:
            EnterBlockLiteral((Production) node);
            break;
        case (int) SmalltalkConstants.ARRAY_LITERAL:
            EnterArrayLiteral((Production) node);
            break;
        case (int) SmalltalkConstants.WORD_ARRAY_LITERAL:
            EnterWordArrayLiteral((Production) node);
            break;
        case (int) SmalltalkConstants.RECEIVER:
            EnterReceiver((Production) node);
            break;
        case (int) SmalltalkConstants.BINARY_SEND:
            EnterBinarySend((Production) node);
            break;
        case (int) SmalltalkConstants.KEYWORD_SEND:
            EnterKeywordSend((Production) node);
            break;
        case (int) SmalltalkConstants.EXPRESSION:
            EnterExpression((Production) node);
            break;
        case (int) SmalltalkConstants.UNARY_SEND:
            EnterUnarySend((Production) node);
            break;
        case (int) SmalltalkConstants.SIMPLE_SEND:
            EnterSimpleSend((Production) node);
            break;
        case (int) SmalltalkConstants.ASSIGN_SEND:
            EnterAssignSend((Production) node);
            break;
        case (int) SmalltalkConstants.MESSAGE:
            EnterMessage((Production) node);
            break;
        }
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public override Node Exit(Node node) {
        switch (node.Id) {
        case (int) SmalltalkConstants.IDENT:
            return ExitIdent((Token) node);
        case (int) SmalltalkConstants.KEYWORD:
            return ExitKeyword((Token) node);
        case (int) SmalltalkConstants.SELECTOR:
            return ExitSelector((Token) node);
        case (int) SmalltalkConstants.STRING:
            return ExitString((Token) node);
        case (int) SmalltalkConstants.CHAR_LITERAL:
            return ExitCharLiteral((Token) node);
        case (int) SmalltalkConstants.NUM_LITERAL:
            return ExitNumLiteral((Token) node);
        case (int) SmalltalkConstants.HASH:
            return ExitHash((Token) node);
        case (int) SmalltalkConstants.LEFT_PAREN:
            return ExitLeftParen((Token) node);
        case (int) SmalltalkConstants.RIGHT_PAREN:
            return ExitRightParen((Token) node);
        case (int) SmalltalkConstants.LEFT_BRACKET:
            return ExitLeftBracket((Token) node);
        case (int) SmalltalkConstants.RIGHT_BRACKET:
            return ExitRightBracket((Token) node);
        case (int) SmalltalkConstants.LEFT_BRACE:
            return ExitLeftBrace((Token) node);
        case (int) SmalltalkConstants.RIGHT_BRACE:
            return ExitRightBrace((Token) node);
        case (int) SmalltalkConstants.COLON:
            return ExitColon((Token) node);
        case (int) SmalltalkConstants.SEMI_COLON:
            return ExitSemiColon((Token) node);
        case (int) SmalltalkConstants.VAR_DELIM:
            return ExitVarDelim((Token) node);
        case (int) SmalltalkConstants.DOT:
            return ExitDot((Token) node);
        case (int) SmalltalkConstants.BINARY:
            return ExitBinary((Token) node);
        case (int) SmalltalkConstants.ASSIGN:
            return ExitAssign((Token) node);
        case (int) SmalltalkConstants.SEQUENCE:
            return ExitSequence((Production) node);
        case (int) SmalltalkConstants.STATEMENT:
            return ExitStatement((Production) node);
        case (int) SmalltalkConstants.SYMBOL_LITERAL:
            return ExitSymbolLiteral((Production) node);
        case (int) SmalltalkConstants.VAR_DEF:
            return ExitVarDef((Production) node);
        case (int) SmalltalkConstants.BLOCK_PARAMS:
            return ExitBlockParams((Production) node);
        case (int) SmalltalkConstants.BLOCK_LITERAL:
            return ExitBlockLiteral((Production) node);
        case (int) SmalltalkConstants.ARRAY_LITERAL:
            return ExitArrayLiteral((Production) node);
        case (int) SmalltalkConstants.WORD_ARRAY_LITERAL:
            return ExitWordArrayLiteral((Production) node);
        case (int) SmalltalkConstants.RECEIVER:
            return ExitReceiver((Production) node);
        case (int) SmalltalkConstants.BINARY_SEND:
            return ExitBinarySend((Production) node);
        case (int) SmalltalkConstants.KEYWORD_SEND:
            return ExitKeywordSend((Production) node);
        case (int) SmalltalkConstants.EXPRESSION:
            return ExitExpression((Production) node);
        case (int) SmalltalkConstants.UNARY_SEND:
            return ExitUnarySend((Production) node);
        case (int) SmalltalkConstants.SIMPLE_SEND:
            return ExitSimpleSend((Production) node);
        case (int) SmalltalkConstants.ASSIGN_SEND:
            return ExitAssignSend((Production) node);
        case (int) SmalltalkConstants.MESSAGE:
            return ExitMessage((Production) node);
        }
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public override void Child(Production node, Node child) {
        switch (node.Id) {
        case (int) SmalltalkConstants.SEQUENCE:
            ChildSequence(node, child);
            break;
        case (int) SmalltalkConstants.STATEMENT:
            ChildStatement(node, child);
            break;
        case (int) SmalltalkConstants.SYMBOL_LITERAL:
            ChildSymbolLiteral(node, child);
            break;
        case (int) SmalltalkConstants.VAR_DEF:
            ChildVarDef(node, child);
            break;
        case (int) SmalltalkConstants.BLOCK_PARAMS:
            ChildBlockParams(node, child);
            break;
        case (int) SmalltalkConstants.BLOCK_LITERAL:
            ChildBlockLiteral(node, child);
            break;
        case (int) SmalltalkConstants.ARRAY_LITERAL:
            ChildArrayLiteral(node, child);
            break;
        case (int) SmalltalkConstants.WORD_ARRAY_LITERAL:
            ChildWordArrayLiteral(node, child);
            break;
        case (int) SmalltalkConstants.RECEIVER:
            ChildReceiver(node, child);
            break;
        case (int) SmalltalkConstants.BINARY_SEND:
            ChildBinarySend(node, child);
            break;
        case (int) SmalltalkConstants.KEYWORD_SEND:
            ChildKeywordSend(node, child);
            break;
        case (int) SmalltalkConstants.EXPRESSION:
            ChildExpression(node, child);
            break;
        case (int) SmalltalkConstants.UNARY_SEND:
            ChildUnarySend(node, child);
            break;
        case (int) SmalltalkConstants.SIMPLE_SEND:
            ChildSimpleSend(node, child);
            break;
        case (int) SmalltalkConstants.ASSIGN_SEND:
            ChildAssignSend(node, child);
            break;
        case (int) SmalltalkConstants.MESSAGE:
            ChildMessage(node, child);
            break;
        }
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterIdent(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitIdent(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterKeyword(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitKeyword(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterSelector(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitSelector(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterString(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitString(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterCharLiteral(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitCharLiteral(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterNumLiteral(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitNumLiteral(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterHash(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitHash(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterLeftParen(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitLeftParen(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterRightParen(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitRightParen(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterLeftBracket(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitLeftBracket(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterRightBracket(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitRightBracket(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterLeftBrace(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitLeftBrace(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterRightBrace(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitRightBrace(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterColon(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitColon(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterSemiColon(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitSemiColon(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterVarDelim(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitVarDelim(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterDot(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitDot(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterBinary(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitBinary(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterAssign(Token node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitAssign(Token node) {
        return node;
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterSequence(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitSequence(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildSequence(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterStatement(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitStatement(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildStatement(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterSymbolLiteral(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitSymbolLiteral(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildSymbolLiteral(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterVarDef(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitVarDef(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildVarDef(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterBlockParams(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitBlockParams(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildBlockParams(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterBlockLiteral(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitBlockLiteral(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildBlockLiteral(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterArrayLiteral(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitArrayLiteral(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildArrayLiteral(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterWordArrayLiteral(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitWordArrayLiteral(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildWordArrayLiteral(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterReceiver(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitReceiver(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildReceiver(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterBinarySend(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitBinarySend(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildBinarySend(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterKeywordSend(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitKeywordSend(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildKeywordSend(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterExpression(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitExpression(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildExpression(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterUnarySend(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitUnarySend(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildUnarySend(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterSimpleSend(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitSimpleSend(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildSimpleSend(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterAssignSend(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitAssignSend(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildAssignSend(Production node, Node child) {
        node.AddChild(child);
    }

    /**
     * <summary>Called when entering a parse tree node.</summary>
     *
     * <param name='node'>the node being entered</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void EnterMessage(Production node) {
    }

    /**
     * <summary>Called when exiting a parse tree node.</summary>
     *
     * <param name='node'>the node being exited</param>
     *
     * <returns>the node to add to the parse tree, or
     *          null if no parse tree should be created</returns>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual Node ExitMessage(Production node) {
        return node;
    }

    /**
     * <summary>Called when adding a child to a parse tree
     * node.</summary>
     *
     * <param name='node'>the parent node</param>
     * <param name='child'>the child node, or null</param>
     *
     * <exception cref='ParseException'>if the node analysis
     * discovered errors</exception>
     */
    public virtual void ChildMessage(Production node, Node child) {
        node.AddChild(child);
    }
}
